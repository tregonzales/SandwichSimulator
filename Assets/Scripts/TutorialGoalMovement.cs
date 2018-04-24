using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGoalMovement : MonoBehaviour {

	public int nextSceneInd;
	// Use this for initialization
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("item")) {
			GameManager.instance.LoadSceneByIndex(nextSceneInd);
		}
	}
}
