using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemBarController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.GetChild(0).GetComponent<Image>().enabled = true;
	}

	public void updateBar(int oldInd, int curInd) {
		transform.GetChild(oldInd).GetComponent<Image>().enabled = false;
		transform.GetChild(curInd).GetComponent<Image>().enabled = true;
	}
}
