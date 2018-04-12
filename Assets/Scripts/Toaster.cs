using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toaster : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>() != null)
        {
            Debug.Log("Enter");
            other.gameObject.GetComponent<Rigidbody>().velocity = (Vector3.up * 20f);
        }
    }
}
