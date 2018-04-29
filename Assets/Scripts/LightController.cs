using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

	public Light[] lighting;
	private bool isColliding;
	private GameObject lastCollider;
    private AudioSource coinDrop;


    // Use this for initialization
    void Start () {
		lighting = FindObjectsOfType (typeof(Light)) as Light[];
		isColliding = false;
        coinDrop = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
		gameObject.GetComponentInParent<Renderer>().material.color = Color.Lerp(Color.yellow, Color.clear, Mathf.PingPong(Time.time, 2));
	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag("item") && !isColliding ) {
            coinDrop.Play();
            lastCollider = other.GetComponentInParent<Rigidbody>().gameObject;
			foreach (Light l in lighting) {
				if (l.intensity == 1f) {
					l.intensity = 0.3f;
				} else {
					l.intensity = 1f;
				}
                Debug.Log(lighting[0].intensity);
            }
            Debug.Log(lastCollider);
			isColliding = true;
		}
	}


	void OnTriggerExit(Collider other) {
		if (other.CompareTag("item") && isColliding && lastCollider == other.GetComponentInParent<Rigidbody>().gameObject) {
            lastCollider = null;
			isColliding = false;
            Debug.Log("HI");
		}
			
	}
}
