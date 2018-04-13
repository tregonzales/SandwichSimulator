using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemBarController : MonoBehaviour {

	// Use this for initialization
	public Color active;
	public Color inactive;
	void Start () {
		transform.GetChild(0).GetComponent<Image>().color = active;
	}

	public void updateBar(int oldInd, int curInd) {
		transform.GetChild(oldInd).GetComponent<Image>().color = inactive;
		transform.GetChild(curInd).GetComponent<Image>().color = active;;
	}
}
