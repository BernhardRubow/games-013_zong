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
        DOTween.Kill(ball, true);
        ball.DOPunchScale(Vector3.one * 1.3f, 1f, 6, 0.25f).OnComplete(()=>{ball.localScale = Vector3.one * 0.75f;});
        ball.DORotate(Vector3.forward * 180 * Mathf.Sign(0.5f -Random.value) ,1f, RotateMode.Fast).OnComplete(()=>{ball.rotation = Quaternion.identity;});
      });

      nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onBallHitsPlayer, (s, e) => {
        DOTween.Kill(ball, true);
        ball.DOPunchScale(Vector3.one * 1.3f, 1f, 6, 0.25f).OnComplete(()=>{ball.localScale = Vector3.one * 0.75f;});
        ball.DORotate(Vector3.forward * 180 * Mathf.Sign(0.5f -Random.value) ,1f, RotateMode.Fast).OnComplete(()=>{ball.rotation = Quaternion.identity;});
      });      

      nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onBallHitsWall, (s, e) => {
        DOTween.Kill(ball, true);
        ball.DOPunchScale(Vector3.one * 1.3f, 1f, 6, 0.25f).OnComplete(()=>{ball.localScale = Vector3.one * 0.75f;});
        ball.DORotate(Vector3.forward * 180 * Mathf.Sign(0.5f -Random.value) ,1f, RotateMode.Fast).OnComplete(()=>{ball.rotation = Quaternion.identity;});
      });
    }
  }
}
