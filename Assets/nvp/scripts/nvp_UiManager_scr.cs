using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using newvisionsproject.managers.events;
using System;

public class nvp_UiManager_scr : MonoBehaviour
{

  // +++ Fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  [SerializeField] Text debugMessage;
  [SerializeField] Text playerOneScoreDisplay;
  [SerializeField] Text playerTwoScoreDisplay;
  [SerializeField] ParticleSystem upperPlayerScoreEffect;
  [SerializeField] ParticleSystem lowerPlayerScoreEffect;




  // +++ life cycle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  void Start()
  {
    // subscribe to events 
    nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onDebugMessage, onDebugMessage);
    nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onPlayerScored, onPlayerScored);
  }




  // +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  void onPlayerScored(object sender, object eventArgs){
    PlayerScore playerScore = (PlayerScore)eventArgs;
    ShowScore(playerScore);
  }

  void onDebugMessage(object sender, object eventArgs)
  {
    if (debugMessage == null)
    {
      debugMessage = GameObject.Find("debugMessage").GetComponent<Text>();
      if (debugMessage == null) Debug.LogError("uiManager: no debug message text found");
    }

    debugMessage.text = eventArgs.ToString();
  }




  // +++ methods ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  void ShowScore(PlayerScore playerScore){
    if(playerScore.PlayerNo == 1)
    {
      playerOneScoreDisplay.text = playerScore.Score.ToString("00");
      lowerPlayerScoreEffect.Play();
    }
    else
    {
      playerTwoScoreDisplay.text = playerScore.Score.ToString("00");
      upperPlayerScoreEffect.Play();
    }
  }
}
