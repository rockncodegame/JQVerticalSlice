using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
	public GameObject p;
	public PlayerStats pStats;
	public MoveTest pMove;
	// Use this for initialization
	void Start () {
		p = GameObject.Find ("Player");
		pStats = p.GetComponent<PlayerStats> ();
		pMove = p.GetComponent<MoveTest> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider c){
		if (c.gameObject.tag == "Player"){
			pMove.checkpoint = transform.position;
		}
	}
}
