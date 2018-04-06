using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.managers.events;

namespace newvisionsproject.zong
{
  public class nvp_BallStartScreen_scr : MonoBehaviour
  {
    // +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void OnTriggerExit(Collider other)
    {      
      // calc a direction that point toward the center of the 
      // screen with some random offset
      Vector3 newDirection = new Vector3	(
        Random.insideUnitCircle.x, 
        Random.insideUnitCircle.y, 
        0)* 6.0f - transform.position;

      // invoke the event that can trigger the direction change inside 
      // the moving state of the ball  
			nvp_EventManager_scr.INSTANCE.InvokeEvent(GameEvents.onChangeDirectionInStartScreen, this, newDirection);
    }

  }

}