using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour {

    private bool isColliding;
    private GameObject lastCollider;
    private AudioSource click;
    private bool isOn;
    private SpriteRenderer benno;

    private void Start()
    {
        isOn = false;
        isColliding = false;
        click = gameObject.GetComponent<AudioSource>();
        benno = GetComponent<SpriteRenderer>();
        benno.enabled = false;
    }


    void OnTriggerEnter(Collider other)
    {
        //if the item that is being collided with is tagged with item, i.e. the piece of bread
        //this is so that other objects such as the corner colliders of the item don't cause repeat triggers
        //and if it is not currently colliding, to avoid repeat triggers as well
        if (other.CompareTag("item") && !isColliding)
        {
            Debug.Log("Dingtard");

            lastCollider = other.GetComponentInParent<Rigidbody>().gameObject; //keeps track of which item is caused the last trigger of this script

            if (isOn)
            {
                benno.enabled = false;
                isOn = false;
            }
            else
            {
                benno.enabled = true;
                isOn = true;
            }

            isColliding = true; //keeps track of if currently colliding

        }

    }

    void OnTriggerExit(Collider other)
    {
        //if the object exiting collision is tagged with item, and the object was currently colliding, and the object that was last colliding is in "other"
        if (other.CompareTag("item") && isColliding && lastCollider == other.GetComponentInParent<Rigidbody>().gameObject)
        {
            lastCollider = null; //there is no longer a collider currently colliding
            isColliding = false; //no longer colliding
        }

    }
}
