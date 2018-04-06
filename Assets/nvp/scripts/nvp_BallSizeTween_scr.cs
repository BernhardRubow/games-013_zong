using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.managers.events;
using DG.Tweening;

namespace newvisionsproject.zong.effects
{
  public class nvp_BallSizeTween_scr : MonoBehaviour
  {

    // +++ private fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    private Transform ball;

    // +++ life cycle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void Start()
    {
      ball = GameObject.Find("ball").transform;
      if (ball == null) Debug.LogError("BallSizeTween: No ball existing");

			// subscribe to events
			nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onChangeDirectionInStartScreen, (s, e) => {
        ball.DOPunchScale(Vector3.one * 1.3f, 1f, 6, 0.25f);
      });

      nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onBallHitsPlayer, (s, e) => {
        ball.DOPunchScale(Vector3.one * 1.3f, 1f, 6, 0.25f);
      });      

      nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onBallHitsWall, (s, e) => {
        ball.DOPunchScale(Vector3.one * 1.3f, 1f, 6, 0.25f);
      });
    }
  }
}
