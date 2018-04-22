using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	CameraController mainCamera;
	public Transform itemHolder;
	private Transform curItem;
	private int curItemIndex;
	private int childCount;
	private bool changing;
	public Transform itemBarTrans;
	itemBarController itemBar;
	GameInputManager gameInputManager;
	public bool paused;
	public bool mainMenu;
	public GameObject controlPanel;
	public GameObject pauseMenu;
	
	int oldInd;

	// Use this for initialization
	void Start () {
		mainCamera = Camera.main.GetComponent<CameraController>();
		curItemIndex = 0;
		if (itemHolder != null) {
			curItem = itemHolder.GetChild(curItemIndex);
			childCount = itemHolder.childCount;
			curItem.GetComponent<PlayerMultiJoint>().enabled = true;
		}
		if (itemBarTrans!= null) {
			itemBar = itemBarTrans.GetComponent<itemBarController>();
		}
		if (!mainMenu) {
			paused = false;
			Time.timeScale = 1.0f;
			pauseMenu.SetActive(paused);
		}
		controlPanel.SetActive(false);
		gameInputManager = GameObject.Find("GameInputManager").GetComponent<GameInputManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!mainMenu) {
			if (gameInputManager.getButton("DpadRight") || Input.GetKeyDown(KeyCode.Space)) {
				changeBar(true);
			}
			else if (gameInputManager.getButton("DpadLeft")) {
				changeBar(false);
			}
		}

		if (Input.GetKeyDown("escape") || (gameInputManager.getButton("B") && paused) || gameInputManager.getButton("start")) {
			if (controlPanel.activeSelf) {
				showControls(false);
			}
			else if (!mainMenu) {
				TogglePauseMenu();
			}
		}
	}

	public void changeBar(bool right) {
		mainCamera.isSwitching = true;
		if (right) {
			oldInd = curItemIndex;
			curItem.GetComponent<PlayerMultiJoint>().enabled = false;
			curItemIndex = (curItemIndex + 1) % childCount;
			curItem = itemHolder.GetChild(curItemIndex);
			mainCamera.target = curItem;
//			mainCamera.updateToSwitchObjects ();
			curItem.GetComponent<PlayerMultiJoint>().enabled = true;
			itemBar.updateBar(oldInd, curItemIndex);
		}
		else {
			oldInd = curItemIndex;
			curItem.GetComponent<PlayerMultiJoint>().enabled = false;
			curItemIndex = curItemIndex == 0 ? childCount - 1 : (curItemIndex - 1) % childCount;
			curItem = itemHolder.GetChild(curItemIndex);
			mainCamera.target = curItem;
//			mainCamera.updateToSwitchObjects ();
			curItem.GetComponent<PlayerMultiJoint>().enabled = true;
			itemBar.updateBar(oldInd, curItemIndex);
		}
		mainCamera.isSwitching = false;

	}

	public void RestartTheGameAfterSeconds(float seconds){
		Time.timeScale = 1.0f;
		StartCoroutine (LoadSceneAfterSeconds (seconds, SceneManager.GetActiveScene ().name));
	}

	public void LoadScene(float seconds, string sceneName){
		StartCoroutine (LoadSceneAfterSeconds (seconds, sceneName));
	}

	public void LoadSceneByIndex(int i){
		SceneManager.LoadScene(i);
	}

	public void LoadMainMenu() {
		SceneManager.LoadScene("Main_Menu");
	}

	public void LoadNextScene(float seconds) {
		StartCoroutine (LoadSceneAfterSeconds (seconds, null));
	}

	public void showControls(bool show) {
		controlPanel.SetActive(show);
	}

	public void TogglePauseMenu() {
		if (paused)
        {
            pauseMenu.SetActive(!paused);
            Time.timeScale = 1.0f;
        }
        else
        {
            pauseMenu.SetActive(!paused);
            Time.timeScale = 0f;
        }
        paused = !paused;
    }

	IEnumerator LoadSceneAfterSeconds(float seconds, string sceneName){
		yield return new WaitForSeconds (seconds);
		if (sceneName == null) {
			SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
		}
		else {
			SceneManager.LoadScene (sceneName);
		}
	}
}
