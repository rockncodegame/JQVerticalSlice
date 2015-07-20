using UnityEngine;
using System.Collections;

public class BossFightControl : MonoBehaviour
{
	public GameObject p;
	public GameObject boss;
	public GameObject eZone;
	public float Bphase;
	public enum States
	{
		Bstate
	};

		// Use this for initialization
		void Start ()
		{
		p = GameObject.Find ("Player");
		//boss = GameObject.Find ("BossWolf");
		//eZone = GameObject.Find ("BossEnemyZone");
		boss.SetActive (false);
		eZone.SetActive (false);
	}
	void OnTriggerEnter (Collider c){
		//when the player enters the zone:
		if (c.gameObject.tag == "Player") {
			boss.GetComponent<BossWolfAI>().delay = Time.time + 3;
			boss.GetComponent<BossWolfAI>().CurrentState = BossWolfAI.States.ResetMove;
			eZone.SetActive(true);
			boss.SetActive(true);
		
		}

	}

	void OnTriggerExit (Collider c){
		//when the player leaves the zone:
		if (c.gameObject.tag == "Player") {
			Bphase = boss.GetComponent<BossWolfAI>().phase;
			//States.Bstate = boss.GetComponent<BossWolfAI>().CurrentState;
			eZone.SetActive(false);
			boss.SetActive(false);
			eZone.GetComponent<BossEnemyZone>().wave = 1;
			boss.GetComponent<BossWolfAI>().delay = Time.time + 2;
			boss.GetComponent<BossWolfAI>().CurrentState = BossWolfAI.States.ResetMove;
			if (Bphase >=1){
			boss.GetComponent<BossWolfAI>().phase = Bphase -1;
			}
		}
		
	}

		// Update is called once per frame
		void Update ()
	{			
				p = GameObject.Find ("Player");
				if (p == null) {
						eZone.SetActive (false);
						boss.SetActive (false);
				}

		}
}

