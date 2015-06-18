using UnityEngine;
using System.Collections;

public class PickUps : MonoBehaviour {
	public int item;
	public GameObject p;
	public PlayerStats pStats;

	// Use this for initialization
	void Start () {
		p = GameObject.Find ("Player");
		pStats = p.GetComponent<PlayerStats> ();
	}


	// Update is called once per frame
	void Update () {

	}

	//1 = health pick up
	//2 = fire pick
	//3 = wind pick
	//4 = elec pick
	//5 = ultimate pick piece
	//6 = rhythm pick up

	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == ("Player")) {
			if (item == 1) {
				pStats.health += 1;
			}
			
			if (item == 2) {
				//have to alter the variable from the class driectly because its a static variable
				PlayerAttack.fire = true;
			}
			
			if (item == 3) {
				PlayerAttack.wind = true;
			}
			
			if (item == 4) {
				PlayerAttack.elec = true;
			}
			
			if (item == 5) {
				PlayerAttack.ult += 1;
			}
			
			if (item == 6) {
				pStats.rhythm += 1;
			}
			Destroy (gameObject);
		}
	}
}
