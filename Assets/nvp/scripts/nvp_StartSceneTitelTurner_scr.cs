using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class nvp_StartSceneTitelTurner_scr : MonoBehaviour {

	// +++ inspector fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	[SerializeField] Transform titelTransform;




	// +++ life cycle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void Start(){
		DOTween.Sequence()
			.AppendInterval(3f)
			.Append(titelTransform.DORotate(new Vector3(0f,0f,180f), 2f, RotateMode.Fast).SetEase(Ease.OutBounce))
			.AppendInterval(3f)
			.Append(titelTransform.DORotate(new Vector3(0f,0f,360f), 2f, RotateMode.Fast).SetEase(Ease.OutBounce))		
			.SetLoops(-1);
	}
}
