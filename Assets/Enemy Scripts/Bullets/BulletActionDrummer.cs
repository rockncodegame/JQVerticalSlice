using UnityEngine;
using System.Collections;

public class BulletActionDrummer : MonoBehaviour {
	//public int shotType;
	//public int speed;
	public Vector3 playerPosition;
	public Vector3 newSize;
	public float speed;
	public Rigidbody rb;
	public GameObject p;
	public float spread;
	public float scaleX;
	// Use this for initialization
	void Start () {
		spread = 3f * Time.deltaTime;
		//set bullet to auto destroy after a time
		p = GameObject.Find ("Player");
		Destroy (gameObject, 2.5f);
		if (p != null) {
			playerPosition = p.transform.position;
			if (transform.position.x > playerPosition.x) {
				speed = -50;
			}
			if (transform.position.x < playerPosition.x) {
				speed = 50;
			}
			rb.AddForce (Vector3.right * speed);
		}
	}
	
	
	// Update is called once per frame
	void Update () {
		playerPosition.y = transform.position.y;
		playerPosition.z = transform.position.z;
		transform.localScale += Vector3.one * spread;
	}
	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "Player") {
			c.gameObject.GetComponent<PlayerStats>().GetHit (1);
			Destroy (gameObject);
			//c.attachedRigidbody.AddForce(new Vector3(1 * dir, 1, 0) * 80);
		}
		
		
	}
}
