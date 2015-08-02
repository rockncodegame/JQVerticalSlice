using UnityEngine;
using System.Collections;

public class MiniZones : MonoBehaviour {
	public GameObject p, enemy;
	public int numEnemies, numSpawned, spawnIndex;
	public float delay, spawnTime;
	public bool spawn, isWaiting;
	public Transform[] spawnPos;
	// Use this for initialization

	void Start () {
		p = GameObject.Find ("player");
		numSpawned = 0;
		spawn = false;
		isWaiting = false;
		spawnTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= spawnTime) {
			spawnTime = Time.time + delay;
			isWaiting = false;
		}
		else{
			isWaiting = true;
		}

		if (spawn && numSpawned < numEnemies) {
				if (!isWaiting){
					Spawn ();
				}
			//StartCoroutine (Spawn (delay));
		}

		if (spawnIndex > spawnPos.Length-1)
			spawnIndex = 0;
	}

	/*IEnumerator Spawn(float d){
		isWaiting = true;
		GameObject dummy;
		spawnIndex = Random.Range (1, 2);
		dummy = Instantiate (enemy, spawnPos[spawnIndex].position, enemy.transform.rotation) as GameObject;
		numSpawned++;

		yield return new WaitForSeconds (d);
		isWaiting = false;
	}*/

	void Spawn(){
		//spawnIndex = Random.Range (1, 2);
		Instantiate (enemy, spawnPos[spawnIndex].position, enemy.transform.rotation);
		numSpawned++;
		spawnIndex++;
	}

	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "Player")
			spawn = true;
	}

	void OnTriggerExit (Collider c){
		if (c.gameObject.tag == "Player") {
			spawn = false;
			numSpawned = 0;
		}
	}
}
