using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using newvisionsproject.managers.events;
using System;
using System.Linq;
using newvisionsproject.zong.interfaces;

namespace newvisionsproject.zong
{


  public class nvp_UiManager_scr : MonoBehaviour
  {

    // +++ inspector fields  ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [SerializeField] Text debugMessage;
    [SerializeField] Text playerOneScoreDisplay;
    [SerializeField] Text playerTwoScoreDisplay;
    [SerializeField] GameObject[] lowerPlayerScoreEffectsHolder;
    [SerializeField] GameObject[] upperPlayerScoreEffectsHolder;




    // +++ private fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    private List<IEffect> lowerPlayerScoreEffects = new List<IEffect>();
    private List<IEffect> upperPlayerScoreEffects= new List<IEffect>();




    // +++ life cycle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void Start()
    {
      // subscribe to events 
      nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onDebugMessage, onDebugMessage);
      nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onPlayerScored, onPlayerScored);

      // grab interface from gameobject because you can't reference interfaces in inspector
      foreach(var item in lowerPlayerScoreEffectsHolder) lowerPlayerScoreEffects.Add(item.GetComponent<IEffect>());
      foreach(var item in upperPlayerScoreEffectsHolder) upperPlayerScoreEffects.Add(item.GetComponent<IEffect>());
    }




    // +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void onPlayerScored(object sender, object eventArgs)
    {
      PlayerScore playerScore = (PlayerScore)eventArgs;
      StartCoroutine(ShowScore(playerScore, 2.5f));
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
    IEnumerator ShowScore(PlayerScore playerScore, float delay)
    {
      yield return new WaitForSeconds(delay);

      nvp_EventManager_scr.INSTANCE.InvokeEvent(GameEvents.onPlayScoringSound, this, null);

      if (playerScore.PlayerNo == 1)
      {
        playerOneScoreDisplay.text = playerScore.Score.ToString("00");
        
        // play all score effects for lower player
        foreach(IEffect effect in lowerPlayerScoreEffects) effect.Play();
      }
      else
      {
        playerTwoScoreDisplay.text = playerScore.Score.ToString("00");

        // play all score effects for upper player
        foreach(IEffect effect in upperPlayerScoreEffects) effect.Play();
      }
    }
  }
}
