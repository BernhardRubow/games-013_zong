using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nvp_StartScene_scr : MonoBehaviour {

	// +++ inspector fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	[SerializeField] Transform _mainImage;
	[SerializeField] float _rotationSpeed;
	[SerializeField] AudioClip[] backgroundMusics;
 



	// +++ life cycle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void Start () {	
		// loop random clips	
		Invoke("PlayRandomSound", 0f);
	}

	void Update () {

		// break condition to load new scene
		if(Input.touchCount > 0){
			SceneManager.LoadScene("game");
		}

		// rotate center image
		_mainImage.Rotate(Vector3.forward, _rotationSpeed * Mathf.Sin(Time.realtimeSinceStartup/2) * Time.deltaTime);
	}

	void PlayRandomSound(){
		var source = this.GetComponent<AudioSource>();
		source.clip = backgroundMusics[Random.Range(0, backgroundMusics.Length)];
		source.Play();
		Invoke("PlayRandomSound", source.clip.length + 0.5f);
	}
}
