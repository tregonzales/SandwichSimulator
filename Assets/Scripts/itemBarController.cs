using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemBarController : MonoBehaviour {

	public CameraController mainCamera;
	public Transform itemHolder;
	private Transform curItem;
	private int curItemIndex;
	private int childCount;
	itemBarController itemBar;
	GameInputManager gameInputManager;
	public bool mainMenu;
	int oldInd;

	// Use this for initialization
	public Color active;
	public Color inactive;
	void Start () {
		transform.GetChild(0).GetComponent<Image>().color = active;

		itemBar = transform.GetComponent<itemBarController> ();

		mainCamera = Camera.main.GetComponent<CameraController>();

		curItemIndex = 0;
		if (itemHolder != null) {
			curItem = itemHolder.GetChild(curItemIndex);
			childCount = itemHolder.childCount;
			curItem.GetComponent<PlayerMultiJoint>().enabled = true;
		}

		gameInputManager = GameObject.Find("GameInputManager").GetComponent<GameInputManager>();


	}

	public void Update() {
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
		transform.GetChild(curInd).GetComponent<Image>().color = active;;
	}

	public void changeBar(bool right) {
		if (right) {
			oldInd = curItemIndex;
			curItem.GetComponent<PlayerMultiJoint>().enabled = false;
			curItemIndex = (curItemIndex + 1) % childCount;
			curItem = itemHolder.GetChild(curItemIndex);
			curItem.GetComponent<PlayerMultiJoint>().enabled = true;
			itemBar.updateBar(oldInd, curItemIndex);
		}
		else {
			oldInd = curItemIndex;
			curItem.GetComponent<PlayerMultiJoint>().enabled = false;
			curItemIndex = curItemIndex == 0 ? childCount - 1 : (curItemIndex - 1) % childCount;
			curItem = itemHolder.GetChild(curItemIndex);
			curItem.GetComponent<PlayerMultiJoint>().enabled = true;
			itemBar.updateBar(oldInd, curItemIndex);
		}
		mainCamera.SwitchTarget( curItem);
	}

}
