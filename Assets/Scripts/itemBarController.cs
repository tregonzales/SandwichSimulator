using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemBarController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
	}

	public void updateBar(int oldInd, int curInd) {
		transform.GetChild(oldInd).GetComponent<SpriteRenderer>().enabled = false;
		transform.GetChild(curInd).GetComponent<SpriteRenderer>().enabled = true;
	}
}
