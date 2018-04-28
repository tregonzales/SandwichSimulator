using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candyMachineController : MonoBehaviour {

	public Transform spawnPoint;
	public GameObject prefab;
	Vector3 dispense;
	public int force;
	private Color actualColor;
	Color randomColor;
	GameObject instantiated;
	private GameObject lastCollider;
	private AudioSource coinDrop;

	// Use this for initialization
	void Start () {
		actualColor = gameObject.GetComponentInChildren<Renderer>().material.color;
		dispense = transform.up + (-1*transform.forward);
		coinDrop = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponentInChildren<Renderer>().material.color = Color.Lerp(Color.yellow, actualColor, Mathf.PingPong(Time.time, 2));
	}

	void OnTriggerEnter(Collider other) {
		if (lastCollider == null) {
			coinDrop.Play();
			lastCollider = other.GetComponentInParent<Rigidbody>().gameObject;
			instantiated = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
			randomColor = Random.ColorHSV();
			randomColor.a = 1f;
			instantiated.GetComponent<Renderer>().material.color = randomColor;
			instantiated.GetComponent<Rigidbody>().velocity += (dispense * force);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.GetComponentInParent<Rigidbody>().gameObject == lastCollider) {
			lastCollider = null;
		}
	}
}
