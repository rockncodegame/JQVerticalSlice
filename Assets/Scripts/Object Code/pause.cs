using UnityEngine;
using System.Collections;

public class pause : MonoBehaviour {	
	public bool isPaused;
	public GameObject PauseMenu;
	void Start() {
		isPaused = false;
		PauseMenu = GameObject.Find ("Pause Menu");
		PauseMenu.SetActive (false);
	}

	void Update () {
		if ((Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.JoystickButton7))  && !isPaused)
			PauseGame ();
	}

	public void PauseGame(){
		PauseMenu.SetActive (true);
		Time.timeScale = 0;
		isPaused = true;
	}

	public void ResumeGame(){
		PauseMenu.SetActive (false);
		Time.timeScale = 1;
		isPaused = false;
	}

	public void MainMenu() {
		ResumeGame ();
		Application.LoadLevel (0);
	}

	public void QuitGame(){
		Application.Quit ();
	}	
}