using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	public GameObject main, lvls, options;
	// Use this for initialization
	void Start () {
		lvls.SetActive (false);
		options.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GoBack(){
		lvls.SetActive (false);
		options.SetActive (false);
		main.SetActive (true);
	}

	public void showLevels() {
		main.SetActive (false);
		options.SetActive (false);
		lvls.SetActive (true);
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
