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
      nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onSpawnPowerUp, OnSpawnPowerUp);
    }

    // +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void OnSpawnPowerUp(object sender, object eventArgs){
      var values = eventArgs as ArrayList;
      var speed = (float)values[0];
      var dir = (Vector3)values[1];
      var pos = (Vector3)values[2];
      SpawnPowerUp(pos, dir, speed);
    }
    






    // +++ functions ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    private void SpawnPowerUp(Vector3 pos, Vector3 dir, float speed)
    {
      // do not spawn on start
      if(speed != 0f) GetPowerUpToSpawn(pos, dir, speed);            
    }

    
    GameObject GetPowerUpToSpawn(Vector3 pos, Vector3 dir, float speed){
      // create power up
      GameObject powerUp = Instantiate(PowerUpEnergy, pos, Quaternion.identity);      
      // set its speed
      powerUp.GetComponent<nvp_PowerUp_scr>().Speed = speed;

      // with same direction as ball
      // only vertical dir reversed
      dir.y *= -1;
      powerUp.GetComponent<nvp_PowerUp_scr>().Direction = dir;
  
      // and return reference
      return powerUp;
    }

   
  }
}
