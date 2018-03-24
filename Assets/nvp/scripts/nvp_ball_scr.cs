using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nvp_ball_scr : MonoBehaviour {

	// +++ inspektor fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	[SerializeField] private float maxHorizontalOffset;
	[SerializeField] private Vector3 direction;
	[SerializeField] private float speed;




	// +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	private System.Action stateTick;




	// +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void Start () {
		ChangeStateUpdate(stateMovingEnter);		
	}
	
	void Update () {
		stateTick();
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "wall"){
			var pos = transform.position;
			pos.x = maxHorizontalOffset * Mathf.Sign(pos.x);
			transform.position = pos;
			direction.x *= -1;
		}
	}




	// +++ functions ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void ChangeStateUpdate(System.Action nextStateAction)
	{
		stateTick = nextStateAction;
	}




	// +++ states +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void stateMovingEnter(){
		ChangeStateUpdate(stateMovingUpdate);
	}
	void stateMovingUpdate(){
		transform.Translate(direction * speed * Time.deltaTime, Space.World);
	}
}
