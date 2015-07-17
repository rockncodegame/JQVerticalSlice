using UnityEngine;
using System.Collections;

public class Music_Trigger1 : MonoBehaviour {

	// Use this for initialization
	public AudioSource Track3;

	void Start () {
		Track3.Stop ();
	}

	void OnTriggerEnter(Collider Gobject)
	{
		if(Gobject.tag == "Player")
		{
			Track3.Play();
		}
	}

	void OnTriggerExit(){


			Track3.Stop();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
