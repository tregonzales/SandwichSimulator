﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemBarController : MonoBehaviour {

	public CameraController mainCamera;
	public Transform itemHolder;
	private Transform curItem;
	private int curItemIndex;
	//how many items the user has access to
	private int childCount;
	GameInputManager gameInputManager;
	public bool mainMenu;
	int oldInd;

	// Use this for initialization
	public Color active;
	public Color inactive;
	void Start () {
		transform.GetChild(0).GetComponent<Image>().color = active;

		mainCamera = Camera.main.GetComponent<CameraController>();

		curItemIndex = 0;

		//set first item of item holder as active controllable item
		if (itemHolder != null) {
			curItem = itemHolder.GetChild(curItemIndex);
			childCount = itemHolder.childCount;
			curItem.GetComponent<PlayerMultiJoint>().enabled = true;
		}

		gameInputManager = GameInputManager.instance;

	}

	public void Update() {
		//switching between items based off of input to the buttons
		if (!mainMenu) {
			if (gameInputManager.getButton("DpadRight") || Input.GetKeyDown(KeyCode.Space)) {
				changeBar(true);
			}
			else if (gameInputManager.getButton("DpadLeft")) {
				changeBar(false);
			}
		}
	}
	public void updateBar(int oldInd, int curInd) {
		transform.GetChild(oldInd).GetComponent<Image>().color = inactive;
		transform.GetChild(curInd).GetComponent<Image>().color = active;
	}

	//changes the item that is currently selected by updating the UI item bar, keeping track of the item index
	//and disabling or enabling the current/old items
	public void changeBar(bool right) {
		if (right) {
			oldInd = curItemIndex;
			curItem.GetComponent<PlayerMultiJoint>().enabled = false;
			curItemIndex = (curItemIndex + 1) % childCount;
			curItem = itemHolder.GetChild(curItemIndex);
			curItem.GetComponent<PlayerMultiJoint>().enabled = true;
			updateBar(oldInd, curItemIndex);
		}
		else {
			oldInd = curItemIndex;
			curItem.GetComponent<PlayerMultiJoint>().enabled = false;
			curItemIndex = curItemIndex == 0 ? childCount - 1 : (curItemIndex - 1) % childCount;
			curItem = itemHolder.GetChild(curItemIndex);
			curItem.GetComponent<PlayerMultiJoint>().enabled = true;
			updateBar(oldInd, curItemIndex);
		}

		//calls the SwitchTarget method on the camera controller with the item to switch to
		//SwitchTarget will subsequently move the camera accordingly
		mainCamera.SwitchTarget(curItem);
	}

}
