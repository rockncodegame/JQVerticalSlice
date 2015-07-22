using UnityEngine;
using System.Collections;

public class BulletActionGuitar : MonoBehaviour {
	//public int shotType;
	//public int speed;
	public Vector3 playerPosition;
	public float speed;
	public Rigidbody rb;
	public GameObject p;
	// Use this for initialization
	void Start () {
		//set bullet to auto destroy after a time
		p = GameObject.Find ("Player");
		Destroy (gameObject, 1.5f);
		if (p != null) {
			playerPosition = p.transform.position;
			if (transform.position.x > playerPosition.x) {
				speed = -500;
			}
			if (transform.position.x < playerPosition.x) {
				speed = 500;
			}
			rb.AddForce (Vector3.right * speed);
		}
	}
	
	
	// Update is called once per frame
	void Update () {
		//playerPosition.y = transform.position.y;
		//playerPosition.z = transform.position.z;

		//transform.position = Vector3.MoveTowards (transform.position, playerPosition, speed);
	}
	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "Player") {
			c.gameObject.GetComponent<PlayerStats>().GetHit (1);
			Destroy (gameObject);
			//c.attachedRigidbody.AddForce(new Vector3(1 * dir, 1, 0) * 80);
		}
		
		
	}
}
