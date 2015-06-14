using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyZone : MonoBehaviour {
	public GameObject cam;
	public CameraFollow cPos;
	public GameObject bar1,bar2, bar3;
	public GameObject enemyType;
	public int numEnemies, numWaves, wave; 
	public float delay;
	public List<GameObject> e;
	public Transform[] spawnPos;
	//grab enemy scripts
	public GameObject Drummer;
	public GameObject Guitar;
	public GameObject Singer;
	public GameObject Enemy;
	private float enemyR;

	// Use this for initialization
	void Start () {
		cam = GameObject.Find ("Main Camera");
		cPos = cam.GetComponent<CameraFollow> ();
		bar1.SetActive (false);
		bar2.SetActive (false);
		bar3.SetActive (false);
		wave = 0;
		delay = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		//if the delay has passed, check to see if the next wave can be started
		//if there are still waves left, and there are no enemies on the field, spawn the next wave
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
		if (wave > numWaves) {
			Reset();
			Destroy(gameObject);
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
		enemyR = Random.Range (1, 6);
		if(enemyR == 1 || enemyR ==2){
			dummy = Instantiate(Drummer, spawnPos [pos].position, transform.rotation) as GameObject;
			e.Add (dummy);
			}
		else if(enemyR == 3 || enemyR ==4){
			dummy = Instantiate(Guitar,spawnPos [pos].position, transform.rotation) as GameObject;
			e.Add (dummy);
			}
		else if(enemyR == 5){
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
