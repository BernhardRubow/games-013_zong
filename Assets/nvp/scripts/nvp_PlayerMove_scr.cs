using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.managers.events;
using newvisionsproject.zong.interfaces;

namespace newvisionsproject.zong
{
  public class nvp_PlayerMove_scr : MonoBehaviour
  {
    // +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
   
    IPlayerInput playerInput;
    public float TargetX;


    // +++ life cycle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void Awake()
    {
      // alternative for constructor injection
      playerInput = this.GetComponent<IPlayerInput>();
    }

    void Update(){
      Vector3 desiredPosition = Vector3.zero;
      desiredPosition.x = playerInput.GetTargetXPosition();
      desiredPosition.y = transform.position.y;

      // move the player in the scene
      //transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 2);
      transform.position = desiredPosition;
    }
  }
}
