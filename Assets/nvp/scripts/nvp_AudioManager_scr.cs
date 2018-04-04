using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.managers.events;

public class nvp_AudioManager_scr : MonoBehaviour {

	// +++ inspector ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	[SerializeField] AudioSource backgroundMusic;
	[SerializeField] AudioSource effects;
	[SerializeField] AudioSource scoringSound;

	[Header("AUDIO CLIPS")]
	[SerializeField] AudioClip[] wallBounce;
	[SerializeField] AudioClip playerBounce;
	[SerializeField] AudioClip[] playerScores;




	// +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void Start () {
		// subscribe to events
		nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onBallHitsWall, onBallHitsWall);
		nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onBallHitsPlayer, onBallHitsPlayer);
		nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onPlayScoringSound, onPlayScoringSound);

		effects.clip = wallBounce[Random.Range(0, wallBounce.Length)];
	}




	// +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void onBallHitsWall(object sender, object eventArgs){
		effects.clip = wallBounce[Random.Range(0, wallBounce.Length)];
		effects.pitch = 1 + Random.Range(-0.1f, 0.1f);
		effects.Play();
	}

	void onBallHitsPlayer(object sender, object eventArgs){		
		effects.clip = playerBounce;
		effects.pitch = 1 + Random.Range(-0.1f, 0.1f);
		effects.Play();
	}

	void onPlayScoringSound(object sender, object eventArgs){
		scoringSound.clip = playerScores[Random.Range(0, playerScores.Length)];		
		scoringSound.pitch = 1 + Random.Range(-0.1f, 0.1f);
		scoringSound.Play();
	}
}
