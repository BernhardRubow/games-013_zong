using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.managers.events;


public class nvp_Player_scr : MonoBehaviour
{
  // +++ public fields ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

  [SerializeField] float force;
  [SerializeField] float damping;



  // +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  Vector3 lowerPlayerXInput = Vector3.zero;
  Vector3 upperPlayerXInput = Vector3.zero;
  Vector2 input;
  Rigidbody rb;
  PlayerPositions playerPosition;             // used to differentiate the player from each other
  System.Action playerTick;
  bool upperPlayerSteering;
  bool lowerPlayerSteering;



  // +++ life cycle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  void Start()
  {
    rb = this.GetComponent<Rigidbody>();

    // differentiate the player the script is running on
    playerPosition = this.transform.position.y > 0 
      ? PlayerPositions.upperPlayer
      : PlayerPositions.lowerPlayer;

    // assing the used update method
    playerTick = PlayerUpdate;
  }

  void Update()
  {
    // get touch input and differentiate the input into values
    // for upper and lower player
    for (int i = 0, n = Input.touchCount; i < n; i++)
    {
      if (Input.GetTouch(i).phase == TouchPhase.Began || Input.GetTouch(i).phase == TouchPhase.Moved)
      {
        input = Input.GetTouch(i).position;
        if (Input.GetTouch(i).position.y > 1250){
          upperPlayerXInput = Camera.main.ScreenToWorldPoint(input);  
        }
        else
        {
          lowerPlayerXInput = Camera.main.ScreenToWorldPoint(input);  
        }
      }
    }

    playerTick();
  }



  // +++ methods ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  void PlayerUpdate()
  {
    // calculate the desired position the player wants to move to
    Vector3 desiredPosition = Vector3.zero;
    desiredPosition.x = playerPosition == PlayerPositions.lowerPlayer ? lowerPlayerXInput.x : upperPlayerXInput.x;
    desiredPosition.y = transform.position.y;

    // move the player in the scene
    transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime*2);
  }
}




/**
* Used to determine on which player (the upper or the lower) 
* the script is currently running on
*/
public enum PlayerPositions
{
  upperPlayer,
  lowerPlayer
}
