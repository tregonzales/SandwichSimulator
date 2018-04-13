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

    void Update () {
        rb.angularVelocity = Vector3.up * speed;
    }
	
}
