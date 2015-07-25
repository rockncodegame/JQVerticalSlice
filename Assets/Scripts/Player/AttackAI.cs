using UnityEngine;
using System.Collections;

public class AttackAI : MonoBehaviour {
	public float speed;
	public float maxDist;
	public float power;
	public float startX, startY, startZ;
	public GameObject p;
	public MoveTest pMove;
	public PlayerAttack pAttack;
	public int dir;
	public bool windL, windR;
	// Use this for initialization
	void Start () {
		startX = transform.position.x;
		startY = transform.position.y;
		startZ = transform.position.z;
		p = GameObject.Find ("Player");
		pMove = p.GetComponent<MoveTest> ();
		pAttack = p.GetComponent<PlayerAttack> ();

		if (pMove.isRight == 1)
			dir = 1;
		if (pMove.isRight == -1)
			dir = -1;

		Destroy (gameObject, 1f);
	}

	// Update is called once per frame
	void Update () {
		if (pAttack.pick == 2) {
			if (windL) {
				if (transform.position.x > (startX - maxDist)) {
					transform.position = new Vector3 (transform.position.x - (speed * Time.deltaTime), startY, startZ);
				}
			}
			if (windR) {
				if (transform.position.x < (startX + maxDist)) {
					transform.position = new Vector3 (transform.position.x + (speed * Time.deltaTime), startY, startZ);
				}
			}				
		}
		else {
			if (dir == 1) {
				if (pAttack.pick == 3) {
					if (transform.position.y > (startY - maxDist)) {
						transform.position = new Vector3 (startX, transform.position.y - (speed * Time.deltaTime), startZ);
					}
				}
				else {
					if (transform.position.x < (startX + maxDist)) {
						transform.position = new Vector3 (transform.position.x + (speed * Time.deltaTime), startY, startZ);
					}
				}
			}
			else {
				if (pAttack.pick == 3) {
					if (transform.position.y > (startY - maxDist)) {
						transform.position = new Vector3 (startX, transform.position.y - (speed * Time.deltaTime), startZ);
					}
				}
				else {
					if (transform.position.x > (startX - maxDist)) {
						transform.position = new Vector3 (transform.position.x - (speed * Time.deltaTime), startY, startZ);
					}
				}
			}
		}
	}

	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "Enemy") {
			if (pAttack.pick == 1)
				c.attachedRigidbody.AddForce(new Vector3(1 * dir, .3f, 0) * 850);
			else if (pAttack.pick == 2)
				c.attachedRigidbody.AddForce(new Vector3(0, 1, 0) * 700);
			else if (pAttack.pick == 3) {
				c.GetComponent<EnemyController>().shockTime = Time.time + 1.0f;
				c.GetComponent<EnemyController>().Shock = 2;
			}
			else {
				c.attachedRigidbody.AddForce(new Vector3(1 * dir, 1, 0) * 80);
			}

			c.gameObject.GetComponent<EnemyController>().GetHit (power);
		}
		//reacting to boss
		if (c.gameObject.tag == "Boss") {
			if (pAttack.pick == 1)
				c.attachedRigidbody.AddForce(new Vector3(1 * dir, .3f, 0) * 850);
			else if (pAttack.pick == 2)
				c.attachedRigidbody.AddForce(new Vector3(0, 1, 0) * 700);
			else if (pAttack.pick == 3) {
				c.GetComponent<BossEnemyController>().shockTime = Time.time + 0.5f;
				c.GetComponent<BossEnemyController>().Shock = 2;
			}
			else {
				c.attachedRigidbody.AddForce(new Vector3(1 * dir, 1, 0) * 80);
			}
			
			c.gameObject.GetComponent<BossEnemyController>().GetHit (power);
		}
	}
}
