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
    public float MaxHorizontalOffset;
    public Vector3 Direction;
    public float Speed;
    public ParticleSystem Sparks;
    public ballConfig ballConfig;  
    public IBallState State;
    public Dictionary<BallStates, IBallState> States;
    




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
      switch(other.tag){
        case "wall":
          nvp_EventManager_scr.INSTANCE.InvokeEvent(GameEvents.onBallHitsWall, this, null);
        break;
        case "player":
          nvp_EventManager_scr.INSTANCE.InvokeEvent(GameEvents.onBallHitsPlayer, this, null);
        break;
        case "powerUpSpawner":
          nvp_EventManager_scr.INSTANCE.InvokeEvent(GameEvents.onHitPowerUpSpawner, this, null);
        break;
      }      
    }




    // +++ methods ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    Dictionary<BallStates, IBallState> CreateStates(GameObject go)
    {
      var s = new Dictionary<BallStates, IBallState>();

      s.Add(BallStates.Moving, new StateMoving(go));
      s.Add(BallStates.outOfBounds, new StateOutOfBounds(go));

      return s;
    }
  }
}
