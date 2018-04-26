using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTutorial : MonoBehaviour {

	public float force;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.R)) {
			GameManager.instance.RestartTheGameAfterSeconds(0.2f);
		}
		else if (Input.GetKey(KeyCode.P)) {
			GameManager.instance.LoadNextScene(0.2f);
		}
		gameObject.GetComponentInParent<Renderer>().material.color = Color.Lerp(Color.yellow, Color.clear, Mathf.PingPong(Time.time, 2));
	}

	void OnTriggerEnter(Collider other){
		if (other.GetComponentInParent<Rigidbody>() != null) {
			other.GetComponentInParent<Rigidbody>().velocity +=  transform.TransformDirection(Vector3.up) * force;
		}
	}
}
