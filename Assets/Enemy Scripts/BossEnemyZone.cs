using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossEnemyZone : MonoBehaviour {
	public GameObject cam;
	public GameObject BossWolf;
	public BossWolfAI BossWolfAI;
	public CameraFollow cPos;
	public GameObject bar1,bar2, bar3;
	public GameObject enemyType;
	public int numEnemies, numWaves, wave; 
	public float delay;
	public float phase;
	public int spawnWheel;
	public List<GameObject> e;
	public Transform[] spawnPos;
	//grab enemy scripts
	public GameObject Drummer;
	public GameObject Guitar;
	public GameObject Singer;
	public GameObject Enemy;
	public bool done;

	// Use this for initialization
	void Start () {
		cam = GameObject.Find ("Main Camera");
		BossWolf = GameObject.FindWithTag("Boss");
		BossWolfAI = BossWolf.GetComponent<BossWolfAI> ();
		cPos = cam.GetComponent<CameraFollow> ();
		bar1.SetActive (true);
		bar2.SetActive (true);
		bar3.SetActive (true);
		wave = 0;
		delay = 1f;
		numWaves = 0;
		done = false;
	}
	
	// Update is called once per frame
	void Update () {
		//if the delay has passed, check to see if the next wave can be started
		//if there are still waves left, and there are no enemies on the field, spawn the next wave
		if (wave < numWaves){
		
		if (Time.time > delay) {
			if ((wave > 0 && wave <= numWaves) && e.Count <= 0) {
				for (int i=0; i < numEnemies; i++){
					
					Spawn (i);
				}
			}
			//if an enemy is destroyed, remove them from the enemy list;
			for (int i=0; i < e.Count; i++){
				if (e[i] == null){
					e.RemoveAt (i);
				}
			}
			//if there are no more enemies, increase the wave number
			if (e.Count <= 0 && wave > 0) {
				delay = Time.time + 3f;
				wave++;
			}
		}

		//if there are no more waves, reset the camera and destroy the wave
		if (wave >= numWaves) {
			//Reset();
			//numWaves = 0;
			//wave = 0;
				done = true;
		}
		}
	}

	void OnTriggerEnter (Collider c){
		//when the player enters the zone:
		//make the field, lock the camera, start the waves, and turn off the trigger
		if (c.gameObject.tag == "Player") {
			cPos.isLocked = true;
			bar1.SetActive (true);
			bar2.SetActive (true);
			bar3.SetActive (true);
			delay = Time.time + 3f;
			wave++;
			gameObject.GetComponent<BoxCollider>().enabled = false;
		}
	}

	public void Spawn(int pos){
		//create a dummy GameObject
		//instantiate the enemy
		//add them to the list
		GameObject dummy;
		//dummy = Instantiate (enemyType, spawnPos [pos].position, Quaternion.identity) as GameObject;
		//e.Add (dummy);
		//decide enemy last number is excluded max
		if (phase ==1 && (e.Count == 0 || e.Count == 2)){
			dummy = Instantiate(Drummer, spawnPos [pos].position, transform.rotation) as GameObject;
			e.Add (dummy);
		}
		if (phase ==1 && (e.Count == 1 || e.Count == 3)){
			dummy = Instantiate(Guitar,spawnPos [pos].position, transform.rotation) as GameObject;
			e.Add (dummy);
		}
		if (phase ==2){
			dummy = Instantiate(Singer, spawnPos [pos].position, transform.rotation) as GameObject;
			e.Add (dummy);
		}
		//phase 3
		spawnWheel++;
		if (spawnWheel >= 4){
			spawnWheel = 1;
		}
		if (phase ==3 && spawnWheel == 1 ){
			dummy = Instantiate(Guitar,spawnPos [pos].position, transform.rotation) as GameObject;
			e.Add (dummy);
		}
		if (phase ==3 && spawnWheel == 2 ){
			dummy = Instantiate(Drummer,spawnPos [pos].position, transform.rotation) as GameObject;
			e.Add (dummy);
		}
		if (phase ==3 && spawnWheel == 3 ){
			dummy = Instantiate(Singer,spawnPos [pos].position, transform.rotation) as GameObject;
			e.Add (dummy);
		}
	}

	public void Reset(){
		//unlock the camera
		//remove the barriers
		cPos.isLocked = false;
		bar1.SetActive (false);
		bar2.SetActive (false);
		bar3.SetActive (false);
	}
}
