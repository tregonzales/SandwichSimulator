using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toaster : MonoBehaviour {

    private Color actual_color;
    private AudioSource coinDrop;


    private void Start()
    {
        actual_color = gameObject.GetComponentInParent<Renderer>().material.color;
        coinDrop = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        gameObject.GetComponentInParent<Renderer>().material.color = Color.Lerp(Color.yellow, actual_color, Mathf.PingPong(Time.time, 2));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>() != null)
        {
            coinDrop.Play();
            other.gameObject.GetComponent<Rigidbody>().velocity = (Vector3.up * 20f);
        }
    }
}
