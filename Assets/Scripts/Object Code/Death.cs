﻿using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {
	public GameObject p;
	public PlayerStats pStats;
	public MoveTest pMove;
	public GameObject[] enemies, zones;
	// Use this for initialization
	void Start () {
		p = GameObject.Find ("Player");
		pStats = p.GetComponent<PlayerStats> ();
		pMove = p.GetComponent<MoveTest> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (pStats.health <= 0)
			StartCoroutine ("Respawn");
	}

	IEnumerator Respawn(){
		pStats.rhythm = 0;
		p.SetActive (false);

		yield return new WaitForSeconds (2f);

		p.SetActive (true);
		pStats.health = 5;
		p.transform.position = pMove.checkpoint;

		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		zones = GameObject.FindGameObjectsWithTag ("EnemyZone");

		foreach (GameObject e in enemies){
			Destroy (e);
		}

		foreach (GameObject z in zones) {
			EnemyZone temp = z.GetComponent<EnemyZone>();
			temp.e.Clear ();
			temp.wave = 0;
			temp.bar1.SetActive (false);
			temp.bar2.SetActive(false);
			temp.bar3.SetActive (false);
			temp.delay = 0f;
			GetComponent<CameraFollow>().isLocked = false;
			z.GetComponent<BoxCollider>().enabled = true;
		}
	}
}