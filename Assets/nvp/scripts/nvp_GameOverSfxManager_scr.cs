using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nvp_GameOverSfxManager_scr : MonoBehaviour {

	// +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	[SerializeField] AudioClip winnerClip;
	[SerializeField] AudioClip loserClip;



	// +++ life cycle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void Start () {
		StartCoroutine(PlayMusicSequence());
	}

	// +++ coroutines +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	IEnumerator PlayMusicSequence(){
		AudioSource audio = this.GetComponent<AudioSource>();

		yield return new WaitForSeconds(0.2f);
		audio.clip = winnerClip;
		audio.Play();

		yield return new WaitForSeconds(winnerClip.length);
		audio.clip = loserClip;
		audio.Play();
		
	}
}
