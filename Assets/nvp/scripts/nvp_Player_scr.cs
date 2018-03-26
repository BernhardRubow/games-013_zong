using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.managers.events;


public class nvp_Player_scr : MonoBehaviour
{

  // +++ public fields ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  public float force;
  public float damping;




  // +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  float ve;
  Vector3 lowerPlayerXInput = Vector3.zero;
  Vector3 upperPlayerXInput = Vector3.zero;
  Vector2 input;
  System.Action playerTick;



  // +++ life cycle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  void Start()
  {
    if (this.transform.position.y > 0)
      playerTick = UpperPlayerUpdate;
    else
      playerTick = LowerPlayerUpdate;
  }

  void Update()
  {
    for (int i = 0, n = Input.touchCount; i < n; i++)
    {
      if (Input.GetTouch(i).phase == TouchPhase.Began || Input.GetTouch(i).phase == TouchPhase.Moved)
      {
        input = Input.GetTouch(i).position;
        if (Input.GetTouch(i).position.y > 1250)
          upperPlayerXInput = Camera.main.ScreenToWorldPoint(input);
        else
          lowerPlayerXInput = Camera.main.ScreenToWorldPoint(input);
      }
    }

    playerTick();
  }




  // +++ functions ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  void LowerPlayerUpdate()
  { 
		Vector3 desiredPosition = Vector3.zero;
    desiredPosition.x = lowerPlayerXInput.x;
		desiredPosition.y = transform.position.y;
    nvp_EventManager_scr.INSTANCE.InvokeEvent(GameEvents.onDebugMessage, this, desiredPosition.x.ToString("000"));
    transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime);
  }

  void UpperPlayerUpdate()
  {
    Vector3 desiredPosition = Vector3.zero;
		desiredPosition.x = upperPlayerXInput.x ;
		desiredPosition.y = transform.position.y;

    transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime);
  }
}
