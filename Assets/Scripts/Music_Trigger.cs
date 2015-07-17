using UnityEngine;
using System.Collections;

public class Music_Trigger : MonoBehaviour {

	// Use this for initialization
	public AudioSource Track1;
	public AudioSource Track2;

	void Start () {
		Track1.Play ();
		Track2.Pause ();
	}

	void OnTriggerEnter(Collider Gobject)
	{
		if(Gobject.tag == "Player")
		{
			Track1.Pause();
			Track2.Play();
		}
	}

	void OnTriggerExit(){

			Track2.Pause();
			Track1.Play();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
