using UnityEngine;
using System.Collections;

public class BulletAction : MonoBehaviour {
	//public int shotType;
	//public int speed;
	public Vector3 playerPosition;
	// Use this for initialization
	void Start () {
		//set bullet to auto destroy after a time
		Destroy (gameObject, 0.5f);

	}
	

	// Update is called once per frame
	void Update () {
		playerPosition = (GameObject.Find ("Player").transform.position);
		float speed = 10 * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, playerPosition, speed);
	}
	
}
