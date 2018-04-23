using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nvp_StartScene_scr : MonoBehaviour {

	// +++ inspector fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	[SerializeField] Transform _mainImage;
	[SerializeField] float _rotationSpeed;
 



	// +++ life cycle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void Update () {
		// rotate center image
		_mainImage.Rotate(Vector3.forward, _rotationSpeed * Mathf.Sin(Time.realtimeSinceStartup/2) * Time.deltaTime);
	}




	// +++ UI Callbacks +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	public void OnCreditsClicked(){
		SceneManager.LoadScene("credits");
	}

	public void OnZongClicked(){		
		SceneManager.LoadScene("game");
	}
}
