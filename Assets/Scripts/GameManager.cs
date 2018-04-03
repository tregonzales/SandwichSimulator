﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class GameManager : MonoBehaviour {

	CameraController mainCamera;
	public Transform itemHolder;
	private Transform curIngredient;
	private int curItemIndex;
	private int childCount;
	private bool changing;

	// Use this for initialization
	void Start () {
		mainCamera = Camera.main.GetComponent<CameraController>();
		curItemIndex = 0;
		// itemHolder = GameObject.Find("itemHolder").transform;
		curIngredient = itemHolder.GetChild(curItemIndex);
		childCount = itemHolder.childCount;
		curIngredient.GetComponent<PlayerMultiJoint>().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (XCI.GetDPadDown(XboxDPad.Right)) {
			curIngredient.GetComponent<PlayerMultiJoint>().enabled = false;
			curItemIndex = (curItemIndex + 1) % childCount;
			curIngredient = itemHolder.GetChild(curItemIndex);
			mainCamera.target = curIngredient;
			curIngredient.GetComponent<PlayerMultiJoint>().enabled = true;
		}
		else if (XCI.GetDPadDown(XboxDPad.Left)) {
			curIngredient.GetComponent<PlayerMultiJoint>().enabled = false;
			curItemIndex = curItemIndex == 0 ? childCount - 1 : (curItemIndex - 1) % childCount;
			curIngredient = itemHolder.GetChild(curItemIndex);
			mainCamera.target = curIngredient;
			curIngredient.GetComponent<PlayerMultiJoint>().enabled = true;
		}
	}
}
