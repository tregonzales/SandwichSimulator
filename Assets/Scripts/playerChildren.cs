using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerChildren : MonoBehaviour {

	public bool canGrab;
	public Rigidbody grabbableBody;

	// Use this for initialization
	void Start () {
		Physics.IgnoreCollision(transform.GetComponent<Collider>(), transform.parent.GetComponent<Collider>(), true);
		canGrab = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// private void OnTriggerEnter(Collider other) {
	// 	canGrab = true;
	// 	grabbableBody = other.GetComponent<Rigidbody>();
	// 	Debug.Log(gameObject);
	// 	Debug.Log("enter");

	// }

	// private void OnTriggerExit(Collider other) {
	// 	canGrab = false;
	// 	grabbableBody = null;
	// 	Debug.Log(gameObject);
	// 	Debug.Log("enter");
	// }

	private void OnCollisionEnter(Collision collision) {
		canGrab = true;
		grabbableBody = collision.transform.GetComponent<Rigidbody>();
	}
}
