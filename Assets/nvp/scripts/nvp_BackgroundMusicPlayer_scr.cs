using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nvp_BackgroundMusicPlayer_scr : MonoBehaviour {

	// +++ inspector fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	[SerializeField] AudioClip[] _backgroundMusicClips;
	AudioSource _backgroundMusicSource;
	

	// +++ unity life cycle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void Start () {
		_backgroundMusicSource = this.GetComponent<AudioSource>();

		StartCoroutine(PlayBackgroundMusic());		
	}

	IEnumerator PlayBackgroundMusic(){
		if(_backgroundMusicClips.Length == 0) Debug.LogError("No music clips");
		while(true){
			int index = Random.Range(0, _backgroundMusicClips.Length);
			_backgroundMusicSource.clip = _backgroundMusicClips[index];
			_backgroundMusicSource.Play();

			yield return new WaitForSeconds(_backgroundMusicClips[index].length);
		}
	}
	

}
