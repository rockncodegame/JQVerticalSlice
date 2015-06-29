using UnityEngine;
using System.Collections;

public class BulletAction : MonoBehaviour {
	//public int shotType;
	//public int speed;
	public Vector3 playerPosition;
	public float speed, dir;
	public Rigidbody rb;
	public GameObject p;
	// Use this for initialization
	void Start () {
		//set bullet to auto destroy after a time

		Destroy (gameObject, 3);
		p = GameObject.Find ("Player");
		if (p != null)
			playerPosition = p.transform.position;
		if (transform.position.x > playerPosition.x) {
			dir = -1;
			}
		if (transform.position.x < playerPosition.x) {
			dir = 1;
		}
		rb.AddForce (Vector3.right * (speed * dir));
	}
	

	// Update is called once per frame
	void Update () {
		playerPosition.y = transform.position.y;
		playerPosition.z = transform.position.z;
	}
	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "Player") {
			c.gameObject.GetComponent<PlayerStats>().GetHit (1);
			Destroy (gameObject);
		}
		if (c.gameObject.tag == "Barrier") {
			Destroy (gameObject);
		}

	}
}
