using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.managers.events;

public class nvp_AudioManager_scr : MonoBehaviour {

	// +++ inspector ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	[SerializeField] AudioSource backgroundMusic;
	[SerializeField] AudioSource effects;

	[Header("AUDIO CLIPS")]
	[SerializeField] AudioClip wallBounce;
	[SerializeField] AudioClip playerBounce;




	// +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void Start () {
		// subscribe to events
		nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onBallHitsWall, onBallHitsWall);
		nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onBallHitsPlayer, onBallHitsPlayer);

		effects.clip = wallBounce;
	}




	// +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void onBallHitsWall(object sender, object eventArgs){
		effects.clip = wallBounce;
		effects.pitch = 1 + Random.Range(-0.1f, 0.1f);
		effects.Play();
	}

	void onBallHitsPlayer(object sender, object eventArgs){		
		effects.clip = playerBounce;
		effects.pitch = 1 + Random.Range(-0.1f, 0.1f);
		effects.Play();
	}
}
