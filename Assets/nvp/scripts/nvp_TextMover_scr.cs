using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nvp_TextMover_scr : MonoBehaviour {

	// +++ fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	[SerializeField] TextConfig textConfig;

	// +++ life cycle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.up * textConfig.moveSpeed * Time.deltaTime, Space.World);
	}
}
