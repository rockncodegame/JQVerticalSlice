using UnityEngine;
using System.Collections;

public class NextLevel1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	void OnTriggerEnter(Collider other) 
	{
		Application.LoadLevel ("Level 3");
	}
	// Update is called once per frame
	void Update () {

	}
}
