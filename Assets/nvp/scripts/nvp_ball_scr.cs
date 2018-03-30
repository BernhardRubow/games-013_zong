using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.managers.events;

namespace newvisionsproject.zong
{
  /**
  * This class is responsible for bringing the ball to
  * live. It the class untiy accesses in the game. */
  public class nvp_Ball_scr : MonoBehaviour
  {

    // +++ inspector fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public float MaxHorizontalOffset;
    public Vector3 Direction;
    public float Speed;
    public ParticleSystem Sparks;




    // +++ private fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    private System.Action stateTick;




    // +++ public fields ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public IState State;
    public Dictionary<BallStates, IState> States;




    // +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void Start()
    {
      States = CreateStates(this.gameObject);
      State = States[BallStates.outOfBounds].SetAsNextState();
    }

    void Update()
    {
      // update the currently assigned state 
      State.Tick();
    }

    void OnTriggerEnter(Collider other)
    {
      if (other.tag == "wall") nvp_EventManager_scr.INSTANCE.InvokeEvent(GameEvents.onBallHitsWall, this, null);
      if (other.tag == "player") nvp_EventManager_scr.INSTANCE.InvokeEvent(GameEvents.onBallHitsPlayer, this, null);
    }




    // +++ methods ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    Dictionary<BallStates, IState> CreateStates(GameObject go)
    {
      var s = new Dictionary<BallStates, IState>();

      s.Add(BallStates.Moving, new StateMoving(go));
      s.Add(BallStates.outOfBounds, new StateOutOfBounds(go));

      return s;
    }
  }




  // +++ states +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  public interface IState
  {
    IState SetAsNextState();
    void Tick();
    void OnEnter();   
    void OnUpdate();
    IState OnExitTo(BallStates nextState);
  }

  /**
  * This is an enumeration over the states
  * the ball can adopt */
  public enum BallStates{
    Moving,
    outOfBounds
  }

  /**
  * This state describes the behavior of
  * the ball when it is moving */
  public class StateMoving : IState
  {
    // +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    private float speed;
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
      direction = ballScript.Direction;
      transform = ballScript.transform;
      maxHorizontalOffset = ballScript.MaxHorizontalOffset;      
    }




    // +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void OnBallHitsPlayer(object sender, object eventArgs)
    {
      direction.y = Mathf.Sign(direction.y) * -1;
      direction.x = Random.Range(-2.0f, 2.0f);
      direction.Normalize();
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

    public IState SetAsNextState()
    {
      action = OnEnter;
      return this;
    }

    public void OnEnter()
    {
      nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onBallHitsWall, OnBallHitsWall);
      nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onBallHitsPlayer, OnBallHitsPlayer);

      action = OnUpdate;
      action();
    }

    public void OnUpdate()
    {
      transform.Translate(direction * speed * Time.deltaTime, Space.World);
      CheckTransitionTo_MovingState();
    }

    public IState OnExitTo(BallStates nextState)
    {
      // do any cleanup here

      // unsubscribe form former subscribtions to particular events
      nvp_EventManager_scr.INSTANCE.UnsubscribeFromEvent(GameEvents.onBallHitsWall, OnBallHitsWall);
      nvp_EventManager_scr.INSTANCE.UnsubscribeFromEvent(GameEvents.onBallHitsPlayer, OnBallHitsPlayer);

      // get the next state and return it to provide
      // fluent configuration
      return ballScript.States[nextState];
    }




    // +++ check methods for state transitions ++++++++++++++++++++++++++++++++++++++++++++++++++++
    private void CheckTransitionTo_MovingState()
    {
      if (Mathf.Abs(transform.position.y) > 25)
      {
        nvp_EventManager_scr.INSTANCE.InvokeEvent(GameEvents.onBallOutOfBounds, this, this.transform.position.y);
        ballScript.State = OnExitTo(BallStates.outOfBounds).SetAsNextState();
      }
    }
  }

  /**
  * This state describes the behavior of
  * the ball, when the player misses the
  * ball and it is getting behind the player */
  public class StateOutOfBounds : IState
  {
    // +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    Transform transform;
    System.Action action;
    nvp_Ball_scr ballScript;
    bool exitState;




    // +++ life cycle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public StateOutOfBounds(GameObject go)
    {
      // collect references and store them localy
      // for ease of use
      ballScript = go.GetComponent<nvp_Ball_scr>();
      transform = ballScript.transform;
    }




    // +++ interface methods ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public void Tick()
    {
      action();
    }
    
    public IState SetAsNextState()
    {
      action = OnEnter;
      return this;
    }

    public void OnEnter()
    {   
      // reset position
      transform.position = Vector3.zero;
      exitState = false;

      // start waiting for some amount of time
      ((MonoBehaviour)ballScript).StartCoroutine(Idle());

      // internal state change
      action = OnUpdate;
    }

    public void OnUpdate()
    {
      // check if the condition for transition to the moving
      // state is fullfilled
      if(exitState){
        ballScript.State = OnExitTo(BallStates.Moving).SetAsNextState();
      }
    }

    public IState OnExitTo(BallStates nextState)
    {
      // Do cleanup here

      // get the next state and return it to provide
      // fluent configuration
      return  ballScript.States[nextState];;
    }




    // +++ coroutines +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    IEnumerator Idle(){
      yield return new WaitForSeconds(1);
      exitState = true;
    }
  }
}
