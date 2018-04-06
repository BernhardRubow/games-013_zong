using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.zong;

namespace newvisionsproject.states.ball
{
	  /**
  * This state describes the behavior of
  * the ball, when the player misses the
  * ball and it is getting behind the player */
  public class StateOutOfBounds : IBallState
  {
    // +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    Transform transform;
    System.Action action;
    nvp_Ball_scr ballScript;
    BallConfig ballConfig;
    float originalSpeed;




    // +++ life cycle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public StateOutOfBounds(GameObject go)
    {
      // collect references and store them localy
      // for ease of use
      ballScript = go.GetComponent<nvp_Ball_scr>();
      ballConfig = ballScript.ballConfig;
      transform = ballScript.transform;
    }




    // +++ interface methods ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public void Tick()
    {
      action();
    }
    
    public IBallState SetAsNextState()
    {
      action = OnEnter;
      return this;
    }

    public void OnEnter()
    {   
      // reset position
      transform.position = Vector3.zero;
      transform.localScale = Vector3.one * 0.75f;

      // start waiting for some amount of time
      ((MonoBehaviour)ballScript).StartCoroutine(Idle());

      // internal state change
      action = OnUpdate;
    }

    public void OnUpdate()
    {
      // do nothing
    }

    public IBallState OnExitTo(BallStates nextState)
    {
      // Do cleanup here

      // get the next state and return it to provide
      // fluent configuration
      return  ballScript.States[nextState];;
    }




    // +++ coroutines +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    IEnumerator Idle(){
      yield return new WaitForSeconds(ballConfig.TimeUntilNewBallStarts);

      // do state transition
      ballScript.State = OnExitTo(BallStates.Moving).SetAsNextState();
    }
  }
}
