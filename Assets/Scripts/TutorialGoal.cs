using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGoal : MonoBehaviour {


	void Update() {
		if (Input.GetKey(KeyCode.R)) {
			GameManager.instance.RestartTheGameAfterSeconds(0.2f);
		}
		else if (Input.GetKey(KeyCode.P)) {
			GameManager.instance.LoadNextScene(0.2f);
		}
	}
	// Use this for initialization
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("item")) {
			GameManager.instance.LoadNextScene(0.2f);
		}
	}
}
