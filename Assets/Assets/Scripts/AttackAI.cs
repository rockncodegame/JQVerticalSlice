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
	// Use this for initialization
	void Start () {
		maxDist = 1.0f;
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
	}

	// Update is called once per frame
	void Update () {
		if (dir == 1) {
			if (pAttack.combo == 3)
				maxDist = 1.5f;
			if (transform.position.x < (startX + maxDist)) {
				transform.position = new Vector3 (transform.position.x + (speed * Time.deltaTime), startY, startZ);
			}
		}
		else {
			if (pAttack.combo == 3)
				maxDist = 1.5f;
			if (transform.position.x > (startX - maxDist)) {
				transform.position = new Vector3 (transform.position.x - (speed * Time.deltaTime), startY, startZ);
			}
		}
		if (transform.position.x < p.transform.position.x + 1.5f) {
			//transform.position = new Vector3 (p.transform.position.x + 2.5f, transform.position.y, transform.position.z);
		}
	}

	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "Enemy") {
			if (pAttack.combo == 3){
				c.attachedRigidbody.AddForce(new Vector3(1 * dir, 1, 0) * 200);
			}
			else {
				c.attachedRigidbody.AddForce(new Vector3(1 * dir, 1, 0) * 80);
			}

			c.gameObject.GetComponent<EnemyController>().GetHit (power);
		}
	}
}
