using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using newvisionsproject.managers.events;

namespace newvisionsproject.zong.effects
{
  public class nvp_ScoreTextEffectManager_scr : MonoBehaviour
  {

    [SerializeField] GameObject scoreEffectGo;
    [SerializeField] Text scoreEffectText;
		[SerializeField] Color Color1;
		[SerializeField] Color Color2;


    Vector3 resetPosition;
    Quaternion resetRotation;
    Vector3 resetAlpha;

    void Awake()
    {
      DOTween.Init();
    }

    void Start()
    {
      nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onPlayerScored, OnPlayerScored);
    }

    void OnPlayerScored(object sender, object eventArgs)
    {
      PlayerScore playerScore = (PlayerScore)eventArgs;

      resetPosition = scoreEffectGo.transform.position;
      resetRotation = scoreEffectGo.transform.rotation;

			scoreEffectText.text = playerScore.Score.ToString("00");

      if (playerScore.PlayerNo == 1) PlayLowerPlayerScoreAnimation();
      else PlayUpperPlayerScoreAnimation();
    }


    void PlayUpperPlayerScoreAnimation()
    {
      scoreEffectText.color = Color2;
      PlayAnimation(-13.3f, 27.4f);

    }
    void PlayLowerPlayerScoreAnimation()
    {
      scoreEffectText.color = Color1;
      PlayAnimation(13.3f, -27.4f);
    }

    void PlayAnimation(float horizontalEndpos, float verticalEndPos)
    {
      Sequence scoreUpperPlayerSeq = DOTween.Sequence().OnComplete(OnCompleteSequence);
      scoreUpperPlayerSeq.Insert(0.0f, scoreEffectGo.transform.DOMove(Vector3.zero, 1.5f, false).SetEase(Ease.OutBounce));
      scoreUpperPlayerSeq.Insert(0.0f, scoreEffectGo.transform.DOLocalRotate(new Vector3(0f, 0f, 1080f), 3f, RotateMode.FastBeyond360));
      scoreUpperPlayerSeq.Insert(1.5f, scoreEffectGo.transform.DOMove(new Vector3(horizontalEndpos, verticalEndPos, 0.0f), 1.5f, false));
      scoreUpperPlayerSeq.Insert(2.0f, scoreEffectText.DOFade(0.0f, 1.0f));
    }

    void Update()
    {

    }

    void OnCompleteSequence()
    {
      scoreEffectGo.transform.position = resetPosition;
      scoreEffectGo.transform.rotation = resetRotation;
    }
  }
}