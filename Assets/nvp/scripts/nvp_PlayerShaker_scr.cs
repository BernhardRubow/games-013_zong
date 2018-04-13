using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.managers.events;
using DG.Tweening;

namespace newvisionsproject.zong.effects
{

  public class nvp_PlayerShaker_scr : MonoBehaviour
  {

    // +++ life cycle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void Start()
    {
			nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onBallHitsPlayer, onBallHitsPlayer);
    }


		// +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		void onBallHitsPlayer(object sender, object boxedTransform){
      Transform transform = (Transform)boxedTransform;
      
      if(transform.position.y > 0){
        DOTween.Sequence()
          .Insert(0, transform.DOPunchRotation(Vector3.forward * 25f, 2f, 3, 1f))
          .Insert(0, transform.DOPunchPosition(Vector3.up, 2f,2,0f,false));
          
      }
      else {
        DOTween.Sequence()
          .Insert(0, transform.DOPunchRotation(Vector3.forward * 25f, 2f, 3, 1f))
          .Insert(0, transform.DOPunchPosition(Vector3.up*-1f, 2f,2,0f,false));
      }
		}


  }

}
