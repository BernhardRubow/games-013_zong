using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nvp_StartScene_scr : MonoBehaviour {

	[SerializeField] Transform _mainImage;
	[SerializeField] float _rotationSpeed;
	[SerializeField] AudioClip[] backgroundMusics;
 
	// Use this for initialization
	void Start () {
		var source = this.GetComponent<AudioSource>();
		source.clip = backgroundMusics[Random.Range(0, backgroundMusics.Length)];
		source.Play();
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.touchCount > 0){
			SceneManager.LoadScene("game");
		}


		_mainImage.Rotate(Vector3.forward, _rotationSpeed * Mathf.Sin(Time.realtimeSinceStartup/2) * Time.deltaTime);
	}
}
