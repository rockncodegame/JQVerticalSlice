using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class pause : MonoBehaviour {	
	public bool isPaused, canChange;
	public GameObject PauseMenu;
	public Button[] btns;
	public int selectedIndex;
	public float cHorizontal;

	void Start() {
		isPaused = false;
		PauseMenu = GameObject.Find ("Pause Menu");
		PauseMenu.SetActive (false);
		isPaused = false;
		selectedIndex = 0;
		canChange = true;
	}

	void Update () {
		cHorizontal = Mathf.Round (Input.GetAxis ("Horizontal"));
		if ((Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.JoystickButton7))  && !isPaused)
			PauseGame ();

		if (Input.GetKeyDown (KeyCode.RightArrow) || cHorizontal > 0) {
			if(canChange && isPaused){
				selectedIndex++;
				canChange = false;
			}
		}
				
		if (Input.GetKeyDown (KeyCode.LeftArrow) || cHorizontal < 0) {
			if(canChange && isPaused){
				selectedIndex--;
				canChange = false;
			}
		}

		if (cHorizontal == 0)
			canChange = true;
		
		if (selectedIndex < 0)
			selectedIndex = 2;

		if (selectedIndex > 2)
			selectedIndex = 0;

		if (isPaused)
			HighlightBtn ();
	}

	public void HighlightBtn() {
		switch (selectedIndex){
		case 0:
			btns[0].Select ();
			if (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.JoystickButton0))
				ResumeGame ();
			break;
		case 1:
			btns[1].Select ();
			if (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.JoystickButton0))
				MainMenu ();
			break;
		case 2:
			btns[2].Select ();
			if (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.JoystickButton0))
				QuitGame ();
			break;
		}
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