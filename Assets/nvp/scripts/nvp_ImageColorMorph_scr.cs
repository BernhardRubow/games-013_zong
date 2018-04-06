using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nvp_ImageColorMorph_scr : MonoBehaviour {

	// +++ inspector fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	[SerializeField] Color from;
	[SerializeField] Color to;
	[SerializeField] float duration;
	[SerializeField] UnityEngine.UI.Image image;

	// +++ private fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	private System.Action tick;
	private float timer = 0;


	// +++ life cylcle ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void Start () {
		image.color = from;
		tick = FromToTo;
	}
	
	// Update is called once per frame
	void Update () {
		tick();
	}




	// +++ state machine ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

	void FromToTo(){
		timer += Time.deltaTime;
		image.color = Color.Lerp(from, to, timer / duration);

		if(timer > duration){
			tick = ToToFrom;
			timer = 0;
		}
	}

	void ToToFrom(){		
		timer += Time.deltaTime;
		image.color = Color.Lerp(to, from, timer / duration);
		
		if(timer > duration){
			tick = FromToTo;
			timer = 0;
		}
	}
}
