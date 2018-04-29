﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class remoteController : MonoBehaviour {

	public Transform tvTransform;
	public int force;
	private Color actualColor;
	private AudioSource click;
	
	// Use this for initialization
	void Start () {
		actualColor = gameObject.GetComponentInChildren<Renderer>().material.color;
		click = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponentInChildren<Renderer>().material.color = Color.Lerp(Color.yellow, actualColor, Mathf.PingPong(Time.time, 2));
	}

	void OnTriggerEnter(Collider other) {
		if (other.GetComponentInParent<Rigidbody>() != null) {
			click.Play();
			other.GetComponentInParent<Rigidbody>().velocity =  
			(tvTransform.position - transform.position).normalized * force;
		}
	}
}
