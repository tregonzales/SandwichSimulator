using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonController : MonoBehaviour {

	private GameObject RB;
	private GameObject LB;
	private GameObject LT;
	private GameObject RT;
	public Color canGrab;
	public Color canGrabItem;
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
		
		;
	}

	public void colorCanGrab (Rigidbody rbGrabbable, Rigidbody lbGrabbable, Rigidbody ltGrabbable, Rigidbody rtGrabbable) {
		if (rbGrabbable != null) {
			RB.GetComponent<Image>().color = rbGrabbable.tag == "item" ? canGrabItem : canGrab;
		}
		else {
			RB.GetComponent<Image>().color = cantGrab;
		}

		if (lbGrabbable != null) {
			LB.GetComponent<Image>().color = lbGrabbable.tag == "item" ? canGrabItem : canGrab;
		}
		else {
			LB.GetComponent<Image>().color = cantGrab;
		}

		if (ltGrabbable != null) {
			LT.GetComponent<Image>().color = ltGrabbable.tag == "item" ? canGrabItem : canGrab;
		}
		else {
			LT.GetComponent<Image>().color = cantGrab;
		}

		if (rtGrabbable != null) {
			RT.GetComponent<Image>().color = rtGrabbable.tag == "item" ? canGrabItem : canGrab;
		}
		else {
			RT.GetComponent<Image>().color = cantGrab;
		}
		// RB.GetComponent<Image>().color = rb ? canGrab : cantGrab;
		// LB.GetComponent<Image>().color = lb ? canGrab : cantGrab;
		// LT.GetComponent<Image>().color = lt ? canGrab : cantGrab;
		// RT.GetComponent<Image>().color = rt ? canGrab : cantGrab;
	}

	public void colorGrabbing (bool rb, bool lb, bool lt, bool rt) {
		if (rb) {
			RB.GetComponent<Image>().color = grabbing;
		}
		if (lb) {
			LB.GetComponent<Image>().color = grabbing;
		}
		if (lt) {
			LT.GetComponent<Image>().color = grabbing;
		}
		if (rt) {
			RT.GetComponent<Image>().color = grabbing;
		}
	}
	
}
