﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

	public Light[] lighting;
	private bool isColliding;
	private GameObject lastCollider;
    private AudioSource coinDrop;

    void Start () {
		lighting = FindObjectsOfType (typeof(Light)) as Light[]; //finds all lights that exist in the current scene
		isColliding = false;
        coinDrop = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
		//makes the lightswitch flash yellow to indicate that it's interactive
		gameObject.GetComponentInParent<Renderer>().material.color = Color.Lerp(Color.yellow, Color.clear, Mathf.PingPong(Time.time, 2));
	}

	void OnTriggerEnter(Collider other){

		//if the item that is being collided with is tagged with item, i.e. the piece of bread
		//this is so that other objects such as the corner colliders of the item don't cause repeat triggers
		//and if it is not currently colliding, to avoid repeat triggers as well
		if (other.CompareTag("item") && !isColliding ) {
            coinDrop.Play(); //adds sound effect
            lastCollider = other.GetComponentInParent<Rigidbody>().gameObject; //keeps track of which item is caused the last trigger of this script

			//turns the lights in the scene either dimmer or brighter based off of prior state
			foreach (Light l in lighting) {
				if (l.intensity == 1f) {
					l.intensity = 0.3f;
				} else {
					l.intensity = 1f;
				}
                Debug.Log(lighting[0].intensity);
            }
            Debug.Log(lastCollider);
			isColliding = true; //keeps track of if currently colliding
		}
	}


	void OnTriggerExit(Collider other) {
		//if the object exiting collision is tagged with item, and the object was currently colliding, and the object that was last colliding is in "other"
		if (other.CompareTag("item") && isColliding && lastCollider == other.GetComponentInParent<Rigidbody>().gameObject) {
            lastCollider = null; //there is no longer a collider currently colliding
			isColliding = false; //no longer colliding
		}
			
	}
}
