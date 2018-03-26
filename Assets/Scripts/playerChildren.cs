using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerChildren : MonoBehaviour {

	public bool canGrab;
	public Rigidbody grabbableBody;

	// Use this for initialization
	void Start () {
		Physics.IgnoreCollision(transform.GetComponent<Collider>(), transform.parent.GetComponent<Collider>());
		canGrab = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other) {
		canGrab = true;
		grabbableBody = other.GetComponent<Rigidbody>();

	}

	void OnTriggerExit(Collider other) {
		canGrab = false;
		grabbableBody = null;
	}
}
