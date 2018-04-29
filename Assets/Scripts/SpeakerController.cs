using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerController : MonoBehaviour {

	//direction to throw item
	Vector3 bounce;
	public int force;
	private Color actualColor; //original color of item prior to flashing yellow
	AudioSource bass;
	// Use this for initialization
	void Start () {
		actualColor = gameObject.GetComponentInParent<Renderer>().material.color;
		bounce = transform.up + (-1*transform.forward);
		bass = transform.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		//flashing yellow
		gameObject.GetComponentInParent<Renderer>().material.color = Color.Lerp(Color.yellow, actualColor, Mathf.PingPong(Time.time, 2));
	}

	void OnTriggerEnter(Collider other){
		if (other.GetComponentInParent<Rigidbody>() != null && other.CompareTag("item")) {
			bass.Play(); //play sound effect
			other.GetComponentInParent<Rigidbody>().velocity +=  transform.TransformDirection(bounce) * force; //push the colliding item with a velocity
		}
	}
}
