using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonController : MonoBehaviour {

	private GameObject RB;
	private GameObject LB;
	private GameObject LT;
	private GameObject RT;
	public Color canGrab;
	public Color cantGrab;
	public Color grabbing;
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

	public void colorCanGrab (bool rb, bool lb, bool lt, bool rt) {
		RB.GetComponent<SpriteRenderer>().color = rb ? canGrab : cantGrab;
		LB.GetComponent<SpriteRenderer>().color = lb ? canGrab : cantGrab;
		LT.GetComponent<SpriteRenderer>().color = lt ? canGrab : cantGrab;
		RT.GetComponent<SpriteRenderer>().color = rt ? canGrab : cantGrab;
	}

	public void colorGrabbing (bool rb, bool lb, bool lt, bool rt) {
		if (rb) {
			RB.GetComponent<SpriteRenderer>().color = grabbing;
		}
		if (lb) {
			LB.GetComponent<SpriteRenderer>().color = grabbing;
		}
		if (lt) {
			LT.GetComponent<SpriteRenderer>().color = grabbing;
		}
		if (rt) {
			RT.GetComponent<SpriteRenderer>().color = grabbing;
		}
	}
	
}
