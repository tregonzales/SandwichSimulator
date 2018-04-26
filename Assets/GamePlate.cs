using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlate : MonoBehaviour {

	Vector3 bounce;
	private Color actualColor;

	// Use this for initialization
	void Start () {
		actualColor = gameObject.GetComponentInParent<Renderer>().material.color;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponentInParent<Renderer>().material.color = Color.Lerp(Color.yellow, actualColor, Mathf.PingPong(Time.time, 2));
	}
}
