using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class GameManager : MonoBehaviour {

	CameraController mainCamera;
	public Transform itemHolder;
	private Transform curItem;
	private int curItemIndex;
	private int childCount;
	private bool changing;
	public Transform itemBarTrans;
	itemBarController itemBar;
	
	int oldInd;

	// Use this for initialization
	void Start () {
		mainCamera = Camera.main.GetComponent<CameraController>();
		curItemIndex = 0;
		curItem = itemHolder.GetChild(curItemIndex);
		childCount = itemHolder.childCount;
		curItem.GetComponent<PlayerMultiJoint>().enabled = true;
		itemBar = itemBarTrans.GetComponent<itemBarController>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (XCI.GetDPadDown(XboxDPad.Right) || Input.GetKeyDown(KeyCode.Space)) {
			oldInd = curItemIndex;
			curItem.GetComponent<PlayerMultiJoint>().enabled = false;
			curItemIndex = (curItemIndex + 1) % childCount;
			curItem = itemHolder.GetChild(curItemIndex);
			mainCamera.target = curItem;
			curItem.GetComponent<PlayerMultiJoint>().enabled = true;
			itemBar.updateBar(oldInd, curItemIndex);
		}
		else if (XCI.GetDPadDown(XboxDPad.Left)) {
			oldInd = curItemIndex;
			curItem.GetComponent<PlayerMultiJoint>().enabled = false;
			curItemIndex = curItemIndex == 0 ? childCount - 1 : (curItemIndex - 1) % childCount;
			curItem = itemHolder.GetChild(curItemIndex);
			mainCamera.target = curItem;
			curItem.GetComponent<PlayerMultiJoint>().enabled = true;
			itemBar.updateBar(oldInd, curItemIndex);
		}
	}
}
