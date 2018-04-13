using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerController : MonoBehaviour {

	Vector3 bounce;
	public int force;
	// Use this for initialization
	void Start () {
		bounce = transform.up + (-1*transform.forward);
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponentInParent<Renderer>().material.color = Color.Lerp(Color.yellow, Color.clear, Mathf.PingPong(Time.time, 2));
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.GetComponentInParent<Rigidbody>() != null) {
			other.gameObject.GetComponentInParent<Rigidbody>().velocity +=  transform.TransformDirection(bounce) * force;
		}
	}
}
