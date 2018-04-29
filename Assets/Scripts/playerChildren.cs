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

	//checks if the corner can grab 
	private void OnTriggerStay(Collider other) {
		if (grabbableBody == null || !canGrab) {	
			canGrab = true;	
			grabbableBody = other.GetComponentInParent<Rigidbody>();
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.GetComponentInParent<Rigidbody>() == grabbableBody) {
			canGrab = false;
			grabbableBody = null;
		}
	}
}
