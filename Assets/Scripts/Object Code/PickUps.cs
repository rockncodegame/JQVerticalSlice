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
	//5-7 = ultimate pick piece
	//8 = rhythm pick up

	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == ("Player")) {

			switch (item) {
			case 1:
				pStats.health += 1;
				break;
			case 2:
				PlayerAttack.fire = true;
				break;
			case 3:
				PlayerAttack.wind = true;
				break;
			case 4:
				PlayerAttack.elec = true;
				break;
			case 5:
				PlayerAttack.ult1 = true;
				break;
			case 6:
				PlayerAttack.ult2 = true;
				break;
			case 7:
				PlayerAttack.ult3 = true;
				break;
			case 8:
				pStats.rhythm += 1;
				break;
			default:
				break;
			}

			Destroy (gameObject);
		}
	}
}
