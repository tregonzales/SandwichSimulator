using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	GameInputManager gameInputManager;
	public bool paused;
	public bool mainMenu;
	public GameObject controlPanel;
	public GameObject pauseMenu;

	void Start () {
		if (!mainMenu) {
			paused = false;
			Time.timeScale = 1.0f;
			pauseMenu.SetActive(paused);
		}
		controlPanel.SetActive(false);
		gameInputManager = GameObject.Find("GameInputManager").GetComponent<GameInputManager>();
	}
	
	void Update () {

		if (Input.GetKeyDown("escape") || (gameInputManager.getButton("B") && paused) || gameInputManager.getButton("start")) {
			if (controlPanel.activeSelf) {
				showControls(false);
			}
			else if (!mainMenu) {
				TogglePauseMenu();
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
