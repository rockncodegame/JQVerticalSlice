using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	public GameObject hud;
	public GameObject h1,h2,h3,h4,h5;
	public GameObject fire, wind, elec, ult;
	public GameObject p;
	public PlayerStats pStats;
	public PlayerAttack pAttack;

	// Use this for initialization
	void Start () {
		p = GameObject.Find ("Player");
		pStats = p.GetComponent<PlayerStats> ();
		//pAttack = p.GetComponent<PlayerAttack> ();

		hud.SetActive (true);
		fire.SetActive (false);
		wind.SetActive (false);
		elec.SetActive (false);
		h1.SetActive (true);
		h2.SetActive (true);
		h3.SetActive (true);
		h4.SetActive (true);
		h5.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
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
