using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	public GameObject hud;
	public GameObject h1,h2,h3,h4,h5;
	public GameObject fire, wind, elec, ult1, ult2, ult3;
	public GameObject p;
	public PlayerStats pStats;
	public PlayerAttack pAttack;

	// Use this for initialization
	void Start () {
		p = GameObject.Find ("Player");
		pStats = p.GetComponent<PlayerStats> ();

		hud.SetActive (true);
		fire.SetActive (false);
		wind.SetActive (false);
		elec.SetActive (false);
		ult1.SetActive (false);
		ult2.SetActive (false);
		ult3.SetActive (false);
		h1.SetActive (true);
		h2.SetActive (true);
		h3.SetActive (true);
		h4.SetActive (true);
		h5.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		CheckPicks ();
		CheckHealth ();		
	}

	void CheckPicks() {
		if (PlayerAttack.fire) {
			fire.SetActive (true);
		}
		else {
			fire.SetActive (false);
		}
		
		if (PlayerAttack.wind) {
			wind.SetActive (true);
		}
		else {
			wind.SetActive (false);
		}
		
		if (PlayerAttack.elec) {
			elec.SetActive (true);
		}
		else {
			elec.SetActive (false);
		}

		if (PlayerAttack.ult1) {
			ult1.SetActive(true);
		}
		else {
			ult1.SetActive(false);
		}

		if (PlayerAttack.ult2) {
			ult2.SetActive(true);
		}
		else {
			ult2.SetActive(false);
		}

		if (PlayerAttack.ult3) {
			ult3.SetActive(true);
		}
		else {
			ult3.SetActive(false);
		}
	}

	void CheckHealth(){
		if (pStats.health < 5) {
			h5.SetActive (false);
		}
		else {
			h5.SetActive (true);
		}
		
		if (pStats.health < 4) {
			h4.SetActive (false);
		}
		else {
			h4.SetActive (true);
		}
		
		if (pStats.health < 3) {
			h3.SetActive (false);
		}
		else {
			h3.SetActive (true);
		}
		
		if (pStats.health < 2) {
			h2.SetActive (false);
		}
		else {
			h2.SetActive (true);
		}
		
		if (pStats.health < 1) {
			h1.SetActive (false);
		}
		else {
			h1.SetActive (true);
		}
	}
}
