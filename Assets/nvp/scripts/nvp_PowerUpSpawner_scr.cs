using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.managers.events;

namespace newvisionsproject.zong
{
  public class nvp_PowerUpSpawner_scr : MonoBehaviour
  {

    // +++ inspector ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    [Header("POWERUPS")]
    [SerializeField] GameObject PowerUpEnergy;




    // +++ private fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    float ballVerticalDirection;


    // Use this for initialization
    void Start()
    {
      // subscribe to events
    }
      

    // +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    
  }
}
