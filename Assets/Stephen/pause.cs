using UnityEngine;
using System.Collections;

public class pause : MonoBehaviour {
	public GUISkin skin;
	
//	private float gldepth = -0.5f;
	private float startTime = 0.1f;
	
	public Material mat;
	
	private long tris = 0;
	private long verts = 0;
	private float savedTimeScale;
	
	private bool showfps;
	private bool showtris;
	private bool showvtx;
	private bool showfpsgraph;
	
	public Color lowFPSColor = Color.red;
	public Color highFPSColor = Color.green;
	
	public int lowFPS = 30;
	public int highFPS = 50;
	
	public GameObject start;
	
	public string url = "unity.html";
	
	public Color statColor = Color.yellow;
	
	public string[] credits = {
		"Jam Quest","Rock N' Code"
	};
	public Texture[] crediticons;
	
	public enum Page {
		None,Main,Options,Credits,MainMenu,Quit
	}
	
	private Page currentPage;
	
	private int toolbarInt = 0;
	private string[]  toolbarstrings =  {"Audio","Graphics","Stats","System"};
	
	
	void Start() {
		Time.timeScale = 1;
	}

	static bool IsDashboard() {
		return Application.platform == RuntimePlatform.OSXDashboardPlayer;
	}
	
	static bool IsBrowser() {
		return (Application.platform == RuntimePlatform.WindowsWebPlayer ||
		        Application.platform == RuntimePlatform.OSXWebPlayer);
	}
	
	void LateUpdate () {
		if (Input.GetKeyDown("escape")) 
		{
			switch (currentPage) 
			{
			case Page.None: 
				PauseGame(); 
				break;
				
			case Page.Main: 
				if (!IsBeginning()) 
					UnPauseGame(); 
				break;
				
			default: 
				currentPage = Page.Main;
				break;
			}
		}
	}
	
	void OnGUI () {
		if (skin != null) {
			GUI.skin = skin;
		}

		if (IsGamePaused()) {
			GUI.color = statColor;
			switch (currentPage) {
			case Page.Main: MainPauseMenu(); break;
			case Page.Options: ShowToolbar(); break;
			case Page.Credits: ShowCredits(); break;
			case Page.Quit: QuitGame(); break;
			case Page.MainMenu: MainMenu (); break;
			}
		}   
	}

	void MainMenu() {
		Application.LoadLevel (0);
	}

	void QuitGame(){
		Application.Quit ();
	}

	void ShowToolbar() {
		BeginPage(300,300);
		toolbarInt = GUILayout.Toolbar (toolbarInt, toolbarstrings);
		switch (toolbarInt) {
		case 0: VolumeControl(); break;
		
		case 1: Qualities(); QualityControl(); break;
		
		}
		EndPage();
	}
	
	void ShowCredits() {
		BeginPage(300,300);
		foreach(string credit in credits) {
			GUILayout.Label(credit);
		}
		foreach( Texture credit in crediticons) {
			GUILayout.Label(credit);
		}
		EndPage();
	}
	
	void ShowBackButton() {
		if (GUI.Button(new Rect(20, Screen.height - 50, 50, 20),"Back")) {
			currentPage = Page.Main;
		}
	}

	void Qualities() {
/*		switch (QualitySettings.currentLevel) 
		{
		case QualityLevel.Fastest:
			GUILayout.Label("Fastest");
			break;
		case QualityLevel.Fast:
			GUILayout.Label("Fast");
			break;
		case QualityLevel.Simple:
			GUILayout.Label("Simple");
			break;
		case QualityLevel.Good:
			GUILayout.Label("Good");
			break;
		case QualityLevel.Beautiful:
			GUILayout.Label("Beautiful");
			break;
		case QualityLevel.Fantastic:
			GUILayout.Label("Fantastic");
			break;
		}*/
	}
	
	void QualityControl() {
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Decrease")) {
			QualitySettings.DecreaseLevel();
		}
		if (GUILayout.Button("Increase")) {
			QualitySettings.IncreaseLevel();
		}
		GUILayout.EndHorizontal();
	}
	
	void VolumeControl() {
		GUILayout.Label("Volume");
		AudioListener.volume = GUILayout.HorizontalSlider(AudioListener.volume, 0, 1);
	}

	void BeginPage(int width, int height) {
		GUILayout.BeginArea( new Rect((Screen.width - width) / 2, (Screen.height - height) / 2, width, height));
	}
	
	void EndPage() {
		GUILayout.EndArea();
		if (currentPage != Page.Main) {
			ShowBackButton();
		}
	}
	
	bool IsBeginning() {
		return (Time.time < startTime);
	}
	
	
	void MainPauseMenu() {
		BeginPage(200,200);
		if (GUILayout.Button (IsBeginning() ? "Play" : "Continue")) {
			UnPauseGame();
			
		}
		if (GUILayout.Button ("Options")) {
			currentPage = Page.Options;
		}
		if (GUILayout.Button ("Credits")) {
			currentPage = Page.Credits;
		}
		if (GUILayout.Button ("Main Menu")) {
			currentPage = Page.MainMenu;
		}
		if (GUILayout.Button ("Quit")) {
			currentPage = Page.Quit;
		}
		if (IsBrowser() && !IsBeginning() && GUILayout.Button ("Restart")) {
			Application.OpenURL(url);
		}
		EndPage();
	}

	void GetObjectStats(GameObject obj) {
		Component[] filters;
		filters = obj.GetComponentsInChildren<MeshFilter>();
		foreach( MeshFilter f  in filters )
		{
			tris += f.sharedMesh.triangles.Length/3;
			verts += f.sharedMesh.vertexCount;
		}
	}
	
	void PauseGame() {
		savedTimeScale = Time.timeScale;
		Time.timeScale = 0;
		AudioListener.pause = true;

		currentPage = Page.Main;
	}
	
	void UnPauseGame() {
		Time.timeScale = savedTimeScale;
		AudioListener.pause = false;
	
		currentPage = Page.None;
		
		if (IsBeginning() && start != null) {
			start.SetActive (true);
		}
	}
	
	bool IsGamePaused() {
		return (Time.timeScale == 0);
	}
	
	void OnApplicationPause(bool pause) {
		if (IsGamePaused()) {
			AudioListener.pause = true;
		}
	}
}