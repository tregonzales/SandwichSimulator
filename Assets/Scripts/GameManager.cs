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
	buttonController buttons;
	public Transform buttonsTrans;
	int oldInd;

	// Use this for initialization
	void Start () {
		mainCamera = Camera.main.GetComponent<CameraController>();
		curItemIndex = 0;
		curItem = itemHolder.GetChild(curItemIndex);
		childCount = itemHolder.childCount;
		curItem.GetComponent<PlayerMultiJoint>().enabled = true;
		itemBar = itemBarTrans.GetComponent<itemBarController>();
		buttons = buttonsTrans.GetComponent<buttonController>();
	}
	
	// Update is called once per frame
	void Update () {
		buttons.updatePositions(curItem.GetChild(1).transform.position, curItem.GetChild(2).transform.position, 
		curItem.GetChild(3).transform.position, curItem.GetChild(4).transform.position);
		if (XCI.GetDPadDown(XboxDPad.Right)) {
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
