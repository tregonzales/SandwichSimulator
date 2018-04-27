using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlate : MonoBehaviour {

	Vector3 bounce;
	public Text counter;
	private Color actualColor;
	private List<GameObject> itemsColected;
	int count;

	// Use this for initialization
	void Start () {
        itemsColected = new List<GameObject>();
		actualColor = gameObject.GetComponentInParent<Renderer>().material.color;
		count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponentInParent<Renderer>().material.color = Color.Lerp(Color.yellow, actualColor, Mathf.PingPong(Time.time, 2));
	}

	void OnTriggerEnter(Collider other) {
		if (!itemsColected.Contains(other.GetComponentInParent<Rigidbody>().gameObject)) {
			itemsColected.Add(other.GetComponentInParent<Rigidbody>().gameObject);
			count++;
			counter.text = count.ToString();
		}
		count++;
		counter.text = count.ToString();
	}
	
	void OnTriggerExit(Collider other) {
		if (itemsColected.Contains(other.GetComponentInParent<Rigidbody>().gameObject)) {
			itemsColected.Remove(other.GetComponentInParent<Rigidbody>().gameObject);
			count--;
			counter.text = count.ToString();
		}
	}
}
