using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerController : MonoBehaviour {

	Vector3 bounce;
	public int force;
	private Color actualColor;
	AudioSource bass;
	// Use this for initialization
	void Start () {
		actualColor = gameObject.GetComponentInParent<Renderer>().material.color;
		bounce = transform.up + (-1*transform.forward);
		bass = transform.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponentInParent<Renderer>().material.color = Color.Lerp(Color.yellow, actualColor, Mathf.PingPong(Time.time, 2));
	}

	void OnTriggerEnter(Collider other){
		if (other.GetComponentInParent<Rigidbody>() != null) {
			bass.Play();
			other.GetComponentInParent<Rigidbody>().velocity +=  transform.TransformDirection(bounce) * force;
		}
	}
}
