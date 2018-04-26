using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

	public Light[] lighting;
	private bool isColliding;
	private GameObject lastCollider;

	// Use this for initialization
	void Start () {
		lighting = FindObjectsOfType (typeof(Light)) as Light[];
		isColliding = false;
	}

	// Update is called once per frame
	void Update () {
		gameObject.GetComponentInParent<Renderer>().material.color = Color.Lerp(Color.yellow, Color.clear, Mathf.PingPong(Time.time, 2));
	}

	void OnTriggerEnter(Collider other){
//		if (!isColliding) {
		if (other.CompareTag("item") && !isColliding) {
			foreach (Light l in lighting) {
				if (l.intensity == 1f) {
					l.intensity = 0.3f;
				} else {
					l.intensity = 1f;
				}
				Debug.Log (l);
			}
//			lastCollider = other.gameObject;
			isColliding = true;
		}
	}

//	void OnTriggerStay(Collider other) {
//		isColliding = true;
//	}
//
	void OnTriggerExit(Collider other) {
//		if (other.gameObject == lastCollider) {
		if (other.CompareTag ("item")) {
			isColliding = false;
		}
			
//			lastCollider = null;
//		}
	}
}
