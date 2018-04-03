using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.managers.events;

namespace newvisionsproject.zong.effects
{
	public class nvp_WallParticleEffect_scr : MonoBehaviour {

		public ParticleSystem leftWallParticleSystem;
		public ParticleSystem rightWallParticleSystem;

		// +++ life cycle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		void Start () {
			nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onBallHitsWall, onBallHitsWall);
		}

		// +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		void onBallHitsWall(object sender, object eventArgs){
			nvp_Ball_scr ball =(nvp_Ball_scr)sender;	
			transform.position = ball.transform.position;
			if(transform.position.x < 0) leftWallParticleSystem.Play();
			else rightWallParticleSystem.Play();
		}
	}
}
