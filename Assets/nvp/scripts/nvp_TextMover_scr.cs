using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class nvp_TextMover_scr : MonoBehaviour {

	// +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	[SerializeField] TextConfig textConfig;

	private Transform childText;
	bool turned;

	// +++ life cycle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void Start () {
		childText = transform.GetChild(0);

		Invoke("BackToStart", 40f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.up * textConfig.moveSpeed * Time.deltaTime, Space.World);

		if(turned) return;

		if(transform.position.y < 0) return;

		turned = true;

		DOTween.Sequence()
		.Insert(0, childText.DORotate(new Vector3(180f, 180f, 0f), 2f, RotateMode.Fast));
		
	}

	void BackToStart(){
		UnityEngine.SceneManagement.SceneManager.LoadScene("start");
	}
}
