using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour {
	public float health;
	public int rhythm;
	public GameObject spark;
	public MoveTest pMove;
	public float inv;
	Animator anim;
	int HitHash = Animator.StringToHash("Hit");
	public GameObject[] rMeter;

	// Use this for initialization
	void Start () {
		health = 5;
		inv = 0;
		pMove = GetComponent<MoveTest> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (health > 5)
			health = 5;
		if (health < 0)
			health = 0;
		if (rhythm < 0)
			rhythm = 0;
		if (rhythm > 8)
			rhythm = 8;
	//rhythm updater
	//deactivates all pieces at multiples of 10 to show meter undereath, and turns on those higher then current rhythm count
	if (rhythm >= 8) {
		for (int i=0; i<8; i++) {
			rMeter[i].SetActive (false);
		}
	} 
	else if (rhythm >= 7) {
		for (int i=0; i<8; i++) {
			if (i < 7){
				rMeter[i].SetActive (false);
			}
			else{
				rMeter[i].SetActive (true);
			}
		}
	}
	else if (rhythm >= 6) {
		for (int i=0; i<8; i++) {
			if (i < 6){
				rMeter[i].SetActive (false);
			}
			else{
				rMeter[i].SetActive (true);
			}
		}
	}
	else if (rhythm >= 5) {
		for (int i=0; i<8; i++) {
			if (i < 5){
				rMeter[i].SetActive (false);
			}
			else{
				rMeter[i].SetActive (true);
			}
		}
	}
	else if (rhythm >= 4) {
		for (int i=0; i<8; i++) {
			if (i < 4){
				rMeter[i].SetActive (false);
			}
			else{
				rMeter[i].SetActive (true);
			}
		}
	}
	else if (rhythm >= 3) {
		for (int i=0; i<8; i++) {
			if (i < 3){
				rMeter[i].SetActive (false);
			}
			else{
				rMeter[i].SetActive (true);
			}
		}
	}
	else if (rhythm >= 2) {
		for (int i=0; i<8; i++) {
			if (i < 2){
				rMeter[i].SetActive (false);
			}
			else{
				rMeter[i].SetActive (true);
			}
		}
	}
	else if (rhythm >= 1) {
		for (int i=0; i<8; i++) {
			if (i < 1){
				rMeter[i].SetActive (false);
			}
			else{
				rMeter[i].SetActive (true);
			}
		}
	}
	else{
		for (int i=0; i<8; i++) {
			rMeter[i].SetActive (true);
		}
	}
}

	public void GetHit(float dmg){
		if (inv < Time.time){
			health -= dmg;
			Instantiate(spark, transform.position, transform.rotation);
			inv = Time.time + 1f;
			anim.SetTrigger (HitHash);
			rhythm -= 8;
		}
	}
}
