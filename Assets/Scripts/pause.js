

var menu = false;
var options = false;
var sound = false;
var video = false;

var sfxVol : int = 6;
var musicVol : int = 6;

var fieldOfView : int = 80;

function OnGUI () {

	if (Input.GetKey("p")){
		menu = true;
	}
	
	if(menu){
		if (GUI.Button(Rect(Screen.width/2 - 50, Screen.height/2, 100, 30), "resume game")){
		// play game
		}

		if (GUI.Button(Rect(Screen.width/2 - 50, Screen.height/2 + 30, 100, 30), "options")){
			menu = false;
			options = true;
		}

		if (GUI.Button(Rect(Screen.width/2 - 50, Screen.height/2 + 90, 100, 30), "quit")){
			Application.Quit();
		}
	}

	if(options){
		if (GUI.Button(Rect(Screen.width/2 - 50, Screen.height/2, 100, 30), "Audio Settings")){
			options = false;
			sound = true;
		}

		if (GUI.Button(Rect(Screen.width/2 - 50, Screen.height/2 + 30, 100, 30), "Video Settings")){
			options = false;
			video = true;
		}

		if (GUI.Button(Rect(Screen.width/2 - 50, Screen.height/2 + 90, 100, 30), "Back")){
			options = false;
			menu = true;
		}
	}


	if(sound){
		sfxVol = GUI.HorizontalSlider (Rect (Screen.width/2 - 50, Screen.height/2, 100, 30), sfxVol, 0.0, 10.0);
		GUI.Label(Rect(Screen.width/2 - 50 + 110, Screen.height/2 - 5, 100, 30), "SFX: " + sfxVol);

		musicVol = GUI.HorizontalSlider (Rect (Screen.width/2 - 50, Screen.height/2 + 30, 100, 30), musicVol, 0.0, 10.0);
		GUI.Label(Rect(Screen.width/2 - 50 + 110, Screen.height/2 + 25, 100, 30), "Music: " + musicVol);

		if (GUI.Button(Rect(Screen.width/2 - 50, Screen.height/2 + 90, 100, 30), "Back")){
			sound = false;
			options = true;
		}
	}

	if(video){
		var qualities = QualitySettings.names;

		GUILayout.BeginVertical ();

		for (var i = 0; i < qualities.Length; i++){
			if (GUI.Button(Rect(Screen.width/2 - 50,  Screen.height/2 - 120 + i * 30, 100, 30), qualities[i])){
				QualitySettings.SetQualityLevel (i, true);
			}
		}

		GUILayout.EndVertical ();

		fieldOfView = GUI.HorizontalSlider (Rect (Screen.width/2 - 50,Screen.height/2 - 150,100,20), fieldOfView, 30, 120);
		GUI.Label(Rect(Screen.width/2 - 50 + 110, Screen.height/2 - 155, 100, 30), "FOV: " + fieldOfView);

		if (GUI.Button(Rect(Screen.width/2 - 50, Screen.height/2 + 90, 100, 30), "Back")){
			video = false;
			options = true;
		}
	}
}

