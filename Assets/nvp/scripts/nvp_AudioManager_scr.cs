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
	[SerializeField] AudioClip[] magicsounds;
	
	




	// +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void Start () {
		// subscribe to events
		nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onBallHitsWall, onBallHitsWall);
		nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onBallHitsPlayer, onBallHitsPlayer);
		nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onPlayScoringSound, onPlayScoringSound);
		nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onPlayMagicSound, onPlayMagicSound);


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
		scoringSound.clip = playerScores[0];		
		scoringSound.pitch = 1 + Random.Range(-0.1f, 0.1f);
		scoringSound.Play();
	}

	void onPlayMagicSound(object sender, object eventArgs){
		AudioSource.PlayClipAtPoint(magicsounds[Random.Range(0, magicsounds.Length)], Camera.main.transform.position);
	}
}
