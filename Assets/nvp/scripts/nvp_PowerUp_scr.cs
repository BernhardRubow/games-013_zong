using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nvp_PowerUp_scr : MonoBehaviour {

	// +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	public float Speed;
	public Vector3 Direction;


	
	// +++ unity callbacks ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void Update () {
		transform.Translate(Direction * Speed * Time.deltaTime, Space.World);

		if(Mathf.Abs(transform.position.y) > 25) Destroy(this.gameObject);
	}




	// +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void OnTriggerEnter(Collider other)
	{
			if(other.tag == "wall")	Direction.x *= -1f;
	}
}
