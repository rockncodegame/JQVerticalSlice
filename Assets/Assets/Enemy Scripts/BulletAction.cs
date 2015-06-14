using UnityEngine;
using System.Collections;

public class BulletAction : MonoBehaviour {
	//public int shotType;
	//public int speed;
	public GameObject p;
	public MoveTest pMove;
	public PlayerStats pStats;
	public float speed;
	//public Vector3 playerPosition;
	// Use this for initialization
	void Start () {
		speed = 8 * Time.deltaTime;
		//set bullet to auto destroy after a time
		Destroy (gameObject, 0.5f);
		p = GameObject.Find ("Player");
		pMove = p.GetComponent<MoveTest> ();
		pStats = p.GetComponent<PlayerStats> ();
	}

	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, p.transform.position, speed);
	}

	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "Player") {
			if (transform.position.x <= p.transform.position.x)
				pMove.movement = new Vector3(130.0f, 150.0f, 0.0f) * Time.deltaTime;
			else
				pMove.movement =  new Vector3(-130.0f, 150.0f, 0.0f) * Time.deltaTime;
			
			pMove.stun = Time.time + .5f;
			pStats.GetHit(1);
		}
	}
	
}
