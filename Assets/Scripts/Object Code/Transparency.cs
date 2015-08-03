using UnityEngine;
using System.Collections;

public class Transparency : MonoBehaviour {
	 public GameObject g;
	// Use this for initialization
	void Start () {
		g.SetActive(true);
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			g.SetActive(false);
		}
	}
	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player") 
		   g.SetActive(true);
		}

	// Update is called once per frame
	void Update () {
	
	}
}
