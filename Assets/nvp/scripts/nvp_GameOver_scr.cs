using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nvp_GameOver_scr : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("Restart", 3.0f);
	}

	void Restart(){
		UnityEngine.SceneManagement.SceneManager.LoadScene("start");
	}
}
