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
		
	}

	void OnTriggerEnter(Collider other){
		if (other.GetComponent<Rigidbody>() != null) {
			Debug.Log("enter");
			other.GetComponent<Rigidbody>().velocity +=  transform.TransformDirection(bounce) * force;
		}
	}
}
