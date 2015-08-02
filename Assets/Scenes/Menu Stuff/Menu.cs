using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
	public GameObject main, lvls;
	public bool showingMain, showingLevels, canChange;
	public int selectedIndex, backIndex;
	public Button[] btns;
	public Button[] levels;
	public Button back;
	public float cHorizontal, cVertical;
	// Use this for initialization
	void Start () {
		lvls.SetActive (false);
		canChange = true;
		selectedIndex = 0;
		backIndex = 0;
		showingMain = true;
		showingLevels = false;
	}
	
	// Update is called once per frame
	void Update () {
		cHorizontal = Mathf.Round (Input.GetAxis ("Horizontal"));
		cVertical = Mathf.Round (Input.GetAxis ("Vertical"));

		if (Input.GetKeyDown (KeyCode.UpArrow) || cVertical > 0){
			if (canChange && showingLevels){
				backIndex++;
				canChange = false;
			}
		}

		if (Input.GetKeyDown (KeyCode.DownArrow) || cVertical < 0){
			if (canChange && showingLevels){
				backIndex--;
				canChange = false;
			}
		}

		if (Input.GetKeyDown (KeyCode.RightArrow) || cHorizontal > 0) {
			if(canChange && showingMain){
				selectedIndex++;
				canChange = false;
			}
		}
		
		if (Input.GetKeyDown (KeyCode.LeftArrow) || cHorizontal < 0) {
			if(canChange && showingMain){
				selectedIndex--;
				canChange = false;
			}
		}
		
		if (cHorizontal == 0 && cVertical == 0)
			canChange = true;
		
		if (selectedIndex < 0)
			selectedIndex = 2;
		
		if (selectedIndex > 2)
			selectedIndex = 0;

		if (backIndex > 1)
			backIndex = 0;

		if (backIndex < 0)
			backIndex = 1;
		
		HighlightBtn ();
	}

	public void HighlightBtn() {
		if (showingMain){
			switch (selectedIndex){
			case 0:
				btns[0].Select ();
				break;
			case 1:
				btns[1].Select ();
				break;
			case 2:
				btns[2].Select ();
				break;
			default:
				break;
			}
		}
		if (showingLevels) {
			if (backIndex == 1){
				back.Select ();
			}
			else {
				for (int i = 0; i < 4; i++){
					if (levels[i].GetComponent<MenuScroll>().pos == 1)
						levels[i].Select ();
				}
			}
		}
	}

	public void GoBack(){
		lvls.SetActive (false);
		main.SetActive (true);
		showingMain = true;
		showingLevels = false;
	}

	public void showLevels() {
		main.SetActive (false);
		lvls.SetActive (true);
		showingLevels = true;
		showingMain = false;
	}

	public void ChooseLevel(int lvl){
		switch (lvl){
			case 1:
				Application.LoadLevel (1);
				break;
			case 2:
				Application.LoadLevel (2);
				break;
			case 3:
				Application.LoadLevel (3);
				break;
			default:
				break;
		}
	}

	public void Quit(){
		Application.Quit ();
	}
}
