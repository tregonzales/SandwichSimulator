using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

	GameInputManager gameInputManager;
	public static GameManager instance;
	public bool paused;
	public bool mainMenu;
	public bool end;
	public bool tutorial;
	public GameObject controlPanel;
	public GameObject pauseMenu;
	public GameObject endGame;
	//plate that holds final items
	public GameObject gamePlate;
	//changing text for end game screen
	public Text endGameCount;
	//ui item counter on normal playing screen
	public GameObject itemCounter;
	//last object before end game screen is active
	private GameObject cameraLast;

	void Start () {
		instance = this;
		if (!mainMenu) {
			GameObject.Find("UIholder").GetComponent<CanvasScaler>().referenceResolution = 
			new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
			paused = false;
			Time.timeScale = 1.0f;
			pauseMenu.SetActive(paused);
		}
		else {
			GameObject.Find("MainMenu").GetComponent<CanvasScaler>().referenceResolution = 
			new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
			paused = true;	
		}
		if (controlPanel != null) {
			controlPanel.SetActive(false);
		}
		if (endGame != null) {
			endGame.SetActive(false);
		}
		end = false;
		gameInputManager = GameInputManager.instance;
	}
	
	void Update () {

		if (Input.GetKeyDown(KeyCode.Escape) || (gameInputManager.getButton("B") && paused) || gameInputManager.getButton("start")) {
			if (controlPanel.activeSelf) {
				showControls(false);
			}
			else if(!mainMenu && !end) {
				TogglePauseMenu();
			}
		}
		if (gameInputManager.getButton("Y") || Input.GetKeyDown(KeyCode.Y)) {
			if (!mainMenu && !paused && !tutorial) {
				ToggleEndGame();
			}
		}
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

	public void ToggleEndGame() {
		if (end)
        {
			//focus the camera back on the last object before the end game screen was active
			EventSystem.current.SetSelectedGameObject(null);
			Camera.main.GetComponent<CameraController>().SwitchTarget(cameraLast.transform);
            endGame.SetActive(!end);
			itemCounter.SetActive(true);
            // Time.timeScale = 1.0f;
        }
        else
        {
			//set camera target to the game plate to show the sandwich the user made and show end game screen
            endGame.SetActive(!end);
			cameraLast = Camera.main.GetComponent<CameraController>().target.gameObject;
			endGameCount.text = itemCounter.transform.GetChild(1).GetComponent<Text>().text;
			itemCounter.SetActive(false);
			Camera.main.GetComponent<CameraController>().SwitchTarget(gamePlate.transform);
			EventSystem.current.SetSelectedGameObject(endGame.transform.GetChild(0).gameObject);
            // Time.timeScale = 0f;
        }
        end = !end;
    }

	public void TogglePauseMenu() {
		if (paused)
        {
			EventSystem.current.SetSelectedGameObject(null);
            pauseMenu.SetActive(!paused);
            Time.timeScale = 1.0f;
        }
        else
        {
            pauseMenu.SetActive(!paused);
			EventSystem.current.SetSelectedGameObject(pauseMenu.transform.GetChild(0).gameObject);
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
