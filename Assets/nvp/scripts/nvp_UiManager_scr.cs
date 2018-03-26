using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using newvisionsproject.managers.events;


public class nvp_UiManager_scr : MonoBehaviour
{

  // +++ Fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  [SerializeField] Text debugMessage;

  // +++ life cycle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  void Start()
  {
    nvp_EventManager_scr.INSTANCE.SubscribeToEvent(
        GameEvents.onDebugMessage,
        onDebugMessage
    );


  }




  // +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  void onDebugMessage(object sender, object eventArgs)
  {
    if (debugMessage == null)
    {
      debugMessage = GameObject.Find("debugMessage").GetComponent<Text>();
      if (debugMessage == null) Debug.LogError("uiManager: no debug message text found");
    }

    debugMessage.text = eventArgs.ToString();

  }

}
