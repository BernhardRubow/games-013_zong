using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nvp_StartScene_scr : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(Input.touchCount);
		if(Input.touchCount > 0){
			SceneManager.LoadScene("game");
		}
	}
}
