using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerChildren : MonoBehaviour {

	public bool canGrab;
	public Rigidbody grabbableBody;

	// Use this for initialization
	void Start () {
		canGrab = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// private void OnTriggerEnter(Collider other) {
	// 	canGrab = true;	
	// 	grabbableBody = other.GetComponent<Rigidbody>();
	// }

	private void OnTriggerStay(Collider other) {
		if (grabbableBody == null || !canGrab) {	
			canGrab = true;	
			grabbableBody = other.GetComponent<Rigidbody>();
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.GetComponent<Rigidbody>() == grabbableBody) {
			canGrab = false;
			grabbableBody = null;
		}
	}
}
