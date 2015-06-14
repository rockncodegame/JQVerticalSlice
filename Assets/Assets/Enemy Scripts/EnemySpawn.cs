using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour
{
	// set up referance and veriables
	private float enemyR;
	public GameObject Singer;
	public GameObject Drummer;
	public GameObject Guitar; 
	public GameObject Enemy;
	public Vector3 dropPoint;
	private float adjuster;
	public Vector3 playerPosition;

		// Use this for initialization
		void Start ()
		{
		//find player position in
		playerPosition = (GameObject.Find ("Player").transform.position);
		}
	
		// Update is called once per frame
		void Update ()
		{
			//looking at playing


		if(Input.GetKeyDown ("q")){
			//sets random number for spawning last number is inclusive
			enemyR = Random.Range (1, 5);

			Spawn();

				}
				// random adjustment to where enemies will spawn off from point
			dropPoint.x = transform.position.x +(Random.Range(-3,3));
			dropPoint.y = transform.position.y +(Random.Range(-1,1));
			dropPoint.z = transform.position.z +(Random.Range(-1,3));
		}
	//spawns depending on the enemyR random variable
		void Spawn(){
		if(enemyR == 1 || enemyR ==2){
			Instantiate(Drummer, dropPoint, transform.rotation);
		}
		else if(enemyR == 3 || enemyR ==4){
			Instantiate(Singer, dropPoint, transform.rotation);
		}
	}



}