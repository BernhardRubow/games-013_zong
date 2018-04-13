using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.managers.events;
using newvisionsproject.states.ball;

namespace newvisionsproject.zong
{
  /**
  * This class is responsible for bringing the ball to
  * live. It the class untiy accesses in the game. */
  public class nvp_Ball_scr : MonoBehaviour
  {

    // +++ public fields ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public BallStates initialState;    
    public BallConfig ballConfig;  
    public IBallState State;
    public Dictionary<BallStates, IBallState> States;

    




    // +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void Start()
    {
      States = CreateStates();
      State = States[initialState].SetAsNextState();
    }

    void Update()
    {
      // update the currently assigned state 
      State.Tick();
    }

    void OnTriggerEnter(Collider other)
    {
      switch(other.tag){        
        case "player":
          nvp_EventManager_scr.INSTANCE.InvokeEvent(GameEvents.onBallHitsPlayer, this, other.transform);
        break;
        case "powerUpSpawner":
          nvp_EventManager_scr.INSTANCE.InvokeEvent(GameEvents.onHitPowerUpSpawner, this, null);
        break;
      }      
    }




    // +++ methods ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    Dictionary<BallStates, IBallState> CreateStates()
    {
      var s = new Dictionary<BallStates, IBallState>();

      // fill states
      s.Add(BallStates.Moving, IBallStateFactory.CreateState(BallStates.Moving, this.gameObject));
      s.Add(BallStates.outOfBounds, IBallStateFactory.CreateState(BallStates.outOfBounds, this.gameObject));

      return s;
    }
  }
}
