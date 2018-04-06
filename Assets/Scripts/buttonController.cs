using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonController : MonoBehaviour {

	private GameObject RB;
	private GameObject LB;
	private GameObject LT;
	private GameObject RT;
	// Use this for initialization
	void Start () {
		RB = transform.GetChild(0).gameObject;
		LB = transform.GetChild(1).gameObject;
		LT = transform.GetChild(2).gameObject;
		RT = transform.GetChild(3).gameObject;
	}

	public void updatePositions (Vector3 rb, Vector3 lb, Vector3 lt, Vector3 rt) {
		RB.GetComponent<RectTransform>().anchoredPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, rb);
		LB.GetComponent<RectTransform>().anchoredPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, lb);
		LT.GetComponent<RectTransform>().anchoredPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, lt);
		RT.GetComponent<RectTransform>().anchoredPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, rt);
	}
	
}
