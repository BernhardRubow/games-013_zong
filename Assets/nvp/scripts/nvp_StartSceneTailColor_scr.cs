using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.managers.events;

public class nvp_StartSceneTailColor_scr : MonoBehaviour
{

  // +++ inspector fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  [SerializeField] Color[] tailColors;




  // +++ private fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  ParticleSystem.MainModule ballTailParticleSystem;




  // +++ life cycle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  void Start()
  {
    // look for a 'ball' object
    GameObject ball = GameObject.Find("ball");
    if (ball == null)
    {
      Debug.LogError("No ball present");
    }

    // get a reference to the particle system on the ball
    Transform temp = ball.transform;
    ballTailParticleSystem = temp.GetChild(0).GetComponent<ParticleSystem>().main;

    // subscribe to interesting events
    nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onChangeDirection, onStartInvertDirection);

  }




  // +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  void onStartInvertDirection(object sender, object eventArgs)
  {
		ballTailParticleSystem.startColor = tailColors[Random.Range(0, tailColors.Length)];
  }

}
