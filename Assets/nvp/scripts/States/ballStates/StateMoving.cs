using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.managers.events;
using newvisionsproject.zong;
using newvisionsproject.zong.gamelogic;

namespace newvisionsproject.states.ball
{
  /**
  * This state describes the behavior of
  * the ball when it is moving */
  public class StateMoving : IBallState
  {
    // +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    private float currentSpeed;
    private Vector3 currentDirection;
    private Transform ballTransform;
    private System.Action currentStateAction;
    private nvp_Ball_scr ballScript;
    BallConfig ballConfig;




    // +++ life cycle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public StateMoving(GameObject go)
    {     
      // get observed component
      ballScript = go.GetComponent<nvp_Ball_scr>();
      ballConfig = ballScript.ballConfig;

      // collect references from the ballscript
      // that are needed to access within this behavior
      currentSpeed = ballConfig.startSpeed;      
      ballTransform = ballScript.transform;
    }




    // +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void OnBallHitsPlayer(object sender, object eventArgs)
    {
      currentDirection = DirectionLogic.CalcRandomBounceFromPlayer(currentDirection);    
      currentSpeed += 1f; 

      ballTransform.position = new Vector3(
        ballTransform.position.x,
        ballConfig.rangeY * Mathf.Sign(ballTransform.position.y),
        0f
      );
    }
    
    void OnBallHitsWall(object sender, object eventArgs)
    {
      currentDirection.x *= -1;
      nvp_EventManager_scr.INSTANCE.InvokeEvent(GameEvents.onBallHitsWall, ballScript, null);
    }

    void OnChangeDirectionByEvent(object sender, object eventArgs){
      this.currentDirection = (Vector3)eventArgs;        
      this.currentDirection.Normalize();
      

        Debug.Log(currentDirection);
    }



    // +++ interface methods ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public void Tick()
    {
      currentStateAction();
    }

    public IBallState SetAsNextState()
    {
      // active state initialisation
      currentStateAction = OnEnter;

      // return this object for fluent configuration
      return this;
    }

    public void OnEnter()
    {
      // reset modified speed to original value
      currentSpeed = ballConfig.startSpeed;

      // subscribe to interesting events
      if(nvp_EventManager_scr.INSTANCE != null) {
        nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onBallHitsPlayer, OnBallHitsPlayer);
        nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onChangeDirectionInStartScreen, OnChangeDirectionByEvent);
      }

      // randomize starting direction
      currentDirection = DirectionLogic.GetRandomDirection();

      // internal state change
      currentStateAction = OnUpdate;
    }

    public void OnUpdate()
    {
      // move the ball
      ballTransform.Translate(currentDirection * currentSpeed * Time.deltaTime, Space.World);

      CheckForWallHit();

      // check possible state transitions
      CheckTransitionTo_OutOfBoundsState();
    }

    public IBallState OnExitTo(BallStates nextState)
    {
      // do any cleanup here

      // unsubscribe form former subscribtions to particular events
      if(nvp_EventManager_scr.INSTANCE != null){
        nvp_EventManager_scr.INSTANCE.UnsubscribeFromEvent(GameEvents.onBallHitsPlayer, OnBallHitsPlayer);
        nvp_EventManager_scr.INSTANCE.UnsubscribeFromEvent(GameEvents.onChangeDirectionInStartScreen, OnChangeDirectionByEvent);
      }
      // get the next state and return it to provide
      // fluent configuration
      return ballScript.States[nextState];
    }




    // +++ check methods for state transitions ++++++++++++++++++++++++++++++++++++++++++++++++++++
    private void CheckTransitionTo_OutOfBoundsState()
    {
      // check if the ball is behind any player
      if (Mathf.Abs(ballTransform.position.y) > 25)
      {
        // inform all interested subscribers, that a player has missed the ball 
        if(nvp_EventManager_scr.INSTANCE != null){
          nvp_EventManager_scr.INSTANCE.InvokeEvent(GameEvents.onBallOutOfBounds, this, this.ballTransform.position.y);
        }

        // transition to next ball state
        ballScript.State = OnExitTo(BallStates.outOfBounds).SetAsNextState();
      }
    }

    void CheckForWallHit(){
      if(Mathf.Abs(ballTransform.position.x) > ballConfig.rangeX){
        ballTransform.position = new Vector3(
          ballConfig.rangeX * Mathf.Sign(ballTransform.position.x),
          ballTransform.position.y,
          ballTransform.position.z
        );
        OnBallHitsWall(this, null);
      }
    }     

  }
}

