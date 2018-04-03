using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.zong.interfaces;

namespace newvisionsproject.zong
{
  public class nvp_PlayerInput_scr : MonoBehaviour, IPlayerInput
  {
		public float maxOffset = 13.72f;
    public float TargetXPos;
    private float playerYPos;




    // +++ life cycle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void Start()
    {
      playerYPos = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
      for (int i = 0, n = Input.touchCount; i < n; i++)
      {
        Vector3 InputWorldPosition = AnalyseTouchInput(i);
        
        // if current touch is a touch from the other player, then check next touch point
        if ((InputWorldPosition.y > 0 && playerYPos > 0) || (InputWorldPosition.y < 0 && playerYPos <0))
        {
          TargetXPos = InputWorldPosition.x;
        }
      }
    }




    // +++ functions ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    Vector3 AnalyseTouchInput(int index)
    {
      Vector3 worldPosition = Vector3.zero;
      if (Input.GetTouch(index).phase == TouchPhase.Began || Input.GetTouch(index).phase == TouchPhase.Moved)
      {
        // get touch position on screen
        Vector2 screenPosition = Input.GetTouch(index).position;

        // assign screen position to world position by using the camera view and the far plane
        // for determing the correct x and y coordinates			
        Vector3 screenPositionWithZPlane = new Vector3(screenPosition.x, screenPosition.y, Camera.main.farClipPlane);
        worldPosition = Camera.main.ScreenToWorldPoint(screenPositionWithZPlane);
      }
      return worldPosition;
    }




    // +++ interface members ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public float GetTargetXPosition()
    {
			TargetXPos = Mathf.Clamp(TargetXPos, -maxOffset, maxOffset);
      return TargetXPos;
    }
  }
}
