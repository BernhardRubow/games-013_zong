using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.managers.events;

namespace newvisionsproject.zong {
  public class nvp_Ball_scr : MonoBehaviour {

    // +++ inspektor fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public float MaxHorizontalOffset;
    public Vector3 Direction;
    public float Speed;
    public ParticleSystem Sparks;




    // +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    private System.Action stateTick;
    private IState state;
    Dictionary<string, IState> states;




    // +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void Start () {
      states = CreateStates(this.gameObject);
      state = states["moving"].SetAsNextState();
    }
    
    void Update () {
      state.Tick();
    }

    void OnTriggerEnter(Collider other){
      if(other.tag == "wall") nvp_EventManager_scr.INSTANCE.InvokeEvent(GameEvents.onBallHitsWall, this, null);
      if(other.tag == "player") nvp_EventManager_scr.INSTANCE.InvokeEvent(GameEvents.onBallHitsPlayer, this, null);
    }	




    // +++ functions ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    Dictionary<string, IState> CreateStates(GameObject go){
      var s = new Dictionary<string, IState>();

      s.Add("moving", new StateMoving(go));

      return s;
    }
  }




  // +++ states +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  public interface IState{
    void Tick();
    
    IState SetAsNextState();
  }

  public class StateMoving : IState
  {
    // +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    private float speed;
    private Vector3 direction;
    private float maxHorizontalOffset;
    private Transform transform;
    private System.Action action;




    // +++ life cycle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public StateMoving(GameObject go){
      // get observed component
      var script = go.GetComponent<nvp_Ball_scr>();

      // collect state references
      speed = script.Speed;
      direction = script.Direction;
      transform = go.transform;
      maxHorizontalOffset = script.MaxHorizontalOffset;

      nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onBallHitsWall, OnBallHitsWall);
      nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onBallHitsPlayer, OnBallHitsPlayer);
    }




    // +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void OnBallHitsPlayer(object sender, object eventArgs){
      direction.y *= -1;
    }
    void OnBallHitsWall(object sender, object eventArgs){
      // var pos = transform.position;
      // pos.x = maxHorizontalOffset * Mathf.Sign(pos.x);
      // transform.position = pos;
      direction.x *= -1;
    }



    // +++ interface methods ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
      public IState SetAsNextState()
      {
          action = OnEnter;
          return this;
      }

      public void Tick()
      {
        action();
      }




      // +++ state actions ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
      void OnEnter(){
        action = OnUpdate;
        action();
      } 

      void OnUpdate(){
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        if(Mathf.Abs(transform.position.y)> 25){

          nvp_EventManager_scr.INSTANCE.InvokeEvent(GameEvents.onBallOutOfBounds, this, this.transform.position.y);

          // reset position
          // TODO: Add extra state
          transform.position = Vector3.zero;
        }
      } 

      void OnExit(){

      }  

  }


}
