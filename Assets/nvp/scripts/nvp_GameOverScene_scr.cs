using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class nvp_GameOverScene_scr : MonoBehaviour {

	// +++ inspector fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	[SerializeField] Transform player1WinningText;
	[SerializeField] Transform player2WinningText;
	[SerializeField] Transform player1LosingText;
	[SerializeField] Transform player2LosingText;




	// +++ life cycle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void Start () {
		if(nvp_GameManager_scr.WINNINGPLAYER == 1) AnimatePlayer1Win();
		else AnimatePlayer2Win();
	}




	// +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void OnComplete(){
		UnityEngine.SceneManagement.SceneManager.LoadScene("start");
	}

	// +++ methods ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void AnimatePlayer1Win(){
		DOTween.Sequence()
			.Append(player1WinningText.transform.DOMove(Vector3.zero, 3f, true))
			.Insert(0f, player1WinningText.transform.DORotate(new Vector3(0f, 0f, 360f * 3), 3f, RotateMode.FastBeyond360))
			.Insert(2.8f, player1WinningText.transform.DOMove(new Vector3(0f, -22.5f, 0f), 0.5f, true).SetEase(Ease.OutBounce))
			.Insert(2.8f, player2LosingText.transform.DOMove(Vector3.zero, 3f, true))
			.Insert(2.8f, player2LosingText.transform.DORotate(new Vector3(0f, 0f, 180f + 360f * 3), 3f, RotateMode.FastBeyond360))
			.Insert(5.6f, player2LosingText.transform.DOMove(new Vector3(0f, 22.5f, 0f), 0.5f, true).SetEase(Ease.OutBounce))
			.AppendInterval(0.5f)
			.OnComplete(OnComplete);
	}

	void AnimatePlayer2Win(){
		DOTween.Sequence()
			.Append(player2WinningText.transform.DOMove(Vector3.zero, 3f, true))
			.Insert(0f, player2WinningText.transform.DORotate(new Vector3(0f, 0f, 180f + 360f * 3), 3f, RotateMode.FastBeyond360))
			.Insert(2.8f, player2WinningText.transform.DOMove(new Vector3(0f, 22.5f, 0f), 0.5f, true).SetEase(Ease.OutBounce))
			.Insert(2.8f, player1LosingText.transform.DOMove(Vector3.zero, 3f, true))
			.Insert(2.8f, player1LosingText.transform.DORotate(new Vector3(0f, 0f, 360f * 3), 3f, RotateMode.FastBeyond360))
			.Insert(5.6f, player1LosingText.transform.DOMove(new Vector3(0f, -22.5f, 0f), 0.5f, true).SetEase(Ease.OutBounce))
			.AppendInterval(0.5f)
			.OnComplete(OnComplete);
	}

}
