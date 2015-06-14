using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {
	public float health;
	public int rhythm;
	// Use this for initialization
	void Start () {
		health = 5;
	}
	
	// Update is called once per frame
	void Update () {
		if (health > 5)
			health = 5;
		if (health < 0)
			health = 0;
	}

	public void GetHit(float dmg){
		health -= dmg;
	}
}
