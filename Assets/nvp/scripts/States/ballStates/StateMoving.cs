using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.managers.events;
using newvisionsproject.zong;

namespace newvisionsproject.states.ball
{
  /**
  * This state describes the behavior of
  * the ball when it is moving */
  public class StateMoving : IBallState
  {
    // +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    private float speed;
    private float originalSpeed;
    private Vector3 direction;
    private float maxHorizontalOffset;
    private Transform transform;
    private System.Action action;
    private nvp_Ball_scr ballScript;




    // +++ life cycle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public StateMoving(GameObject go)
    {     
      // get observed component
      ballScript = go.GetComponent<nvp_Ball_scr>();

      // collect references from the ballscript
      // that are needed to access within this behavior
      speed = ballScript.Speed;
      originalSpeed = speed;
      direction = ballScript.Direction;
      transform = ballScript.transform;
      maxHorizontalOffset = ballScript.MaxHorizontalOffset;  
    }




    // +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void OnHitPowerUpSpawner(object sender, object eventArgs){
      nvp_EventManager_scr.INSTANCE.InvokeEvent(
        GameEvents.onSpawnPowerUp, 
        this, 
        new ArrayList{
          this.speed, 
          this.direction,
          this.transform.position
          });
    }


    void OnBallHitsPlayer(object sender, object eventArgs)
    {
      direction.y = Mathf.Sign(direction.y) * -1;
      direction.x = Random.Range(-2.0f, 2.0f);
      direction.Normalize();

      
      speed += 1f;   
    }
    void OnBallHitsWall(object sender, object eventArgs)
    {
      direction.x *= -1;
    }



    // +++ interface methods ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public void Tick()
    {
      action();
    }

    public IBallState SetAsNextState()
    {
      // active state initialisation
      action = OnEnter;

      // return this object for fluent configuration
      return this;
    }

    public void OnEnter()
    {
      // reset modified speed to original value
      speed = originalSpeed;

      // subscribe to interesting events
      nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onBallHitsWall, OnBallHitsWall);
      nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onBallHitsPlayer, OnBallHitsPlayer);
      nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onHitPowerUpSpawner, OnHitPowerUpSpawner);

      // randomize starting direction
      direction = new Vector3(
        Random.Range(-1.5f, 1.5f),
        Mathf.Sign(0.5f-Random.value),
        0f
      ).normalized;

      // internal state change
      action = OnUpdate;
    }

    public void OnUpdate()
    {
      // move the ball
      transform.Translate(direction * speed * Time.deltaTime, Space.World);

      // check possible state transitions
      CheckTransitionTo_OutOfBoundsState();
    }

    public IBallState OnExitTo(BallStates nextState)
    {
      // do any cleanup here

      // unsubscribe form former subscribtions to particular events
      nvp_EventManager_scr.INSTANCE.UnsubscribeFromEvent(GameEvents.onBallHitsWall, OnBallHitsWall);
      nvp_EventManager_scr.INSTANCE.UnsubscribeFromEvent(GameEvents.onBallHitsPlayer, OnBallHitsPlayer);
      nvp_EventManager_scr.INSTANCE.UnsubscribeFromEvent(GameEvents.onHitPowerUpSpawner, OnHitPowerUpSpawner);

      // get the next state and return it to provide
      // fluent configuration
      return ballScript.States[nextState];
    }




    // +++ check methods for state transitions ++++++++++++++++++++++++++++++++++++++++++++++++++++
    private void CheckTransitionTo_OutOfBoundsState()
    {
      // check if the ball is behind any player
      if (Mathf.Abs(transform.position.y) > 25)
      {
        // inform all interested subscribers, that a player has missed the ball 
        nvp_EventManager_scr.INSTANCE.InvokeEvent(GameEvents.onBallOutOfBounds, this, this.transform.position.y);

        // transition to next ball state
        ballScript.State = OnExitTo(BallStates.outOfBounds).SetAsNextState();
      }
    }
  }
}

