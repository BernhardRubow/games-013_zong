using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.managers.events;

namespace newvisionsproject.zong.effects 
{
	public class nvp_BallEffectManager_scr : MonoBehaviour {

		[SerializeField] ParticleSystem ballParticleSystem;
		[SerializeField] Color upperPlayerColor;
		[SerializeField] Color lowerPlayerColor;

		// Use this for initialization
		void Start () {
			nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onBallHitsPlayer, OnBallHitsPlayer);
		}

		void OnBallHitsPlayer(object sender, object eventArgs){
			var main = ballParticleSystem.main;
			float yPos = ((nvp_Ball_scr)sender).gameObject.transform.position.y;
			if(yPos < 0) main.startColor = lowerPlayerColor;
			else main.startColor = upperPlayerColor;
		}
	}

}
