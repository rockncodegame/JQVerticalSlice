using UnityEngine;
using System.Collections;

public class NextLevel : MonoBehaviour {
	public int level;
	// Use this for initialization
	void Start () {
	
	}
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.tag == "Player")
			Application.LoadLevel (level);
	}
	// Update is called once per frame
	void Update () {

	}
}
