using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour {

    // Use this for initialization
    private Rigidbody rb;
    public int speed;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

    }

    private void Update()
    {
        // Spins the fan constantly for items to hit
        rb.angularVelocity = Vector3.up * speed;
    }



}
