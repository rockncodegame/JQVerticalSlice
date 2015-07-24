using UnityEngine;
using System.Collections;

public class EnemyDespawn : MonoBehaviour {

	public GameObject enemy;
	public GameObject p;
	public float despawnDistance;
	
	// Use this for initialization
	void Start () {
		p = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if ((p.transform.position.x - enemy.transform.position.x > despawnDistance) || (p.transform.position.x - enemy.transform.position.x < (despawnDistance*-1))) {
			Destroy(enemy);
		}
	}
}
