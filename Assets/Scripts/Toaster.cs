using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toaster : MonoBehaviour {

    private Color actual_color; //original color prior to flashing yellow
    private AudioSource spring;


    private void Start()
    {
        actual_color = gameObject.GetComponentInParent<Renderer>().material.color;
        spring = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        //makes the lightswitch flash yellow to indicate that it's interactive
        gameObject.GetComponentInParent<Renderer>().material.color = Color.Lerp(Color.yellow, actual_color, Mathf.PingPong(Time.time, 2));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>() != null)
        {
            spring.Play(); //play sound effect

            // Launches the item up in the air
            other.gameObject.GetComponent<Rigidbody>().velocity = (Vector3.up * 20f);
        }
    }
}
