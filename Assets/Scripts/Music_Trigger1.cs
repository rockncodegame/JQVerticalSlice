using UnityEngine;
using System.Collections;

public class Music_Trigger1 : MonoBehaviour {

	// Use this for initialization
	public AudioSource Track3;
	public AudioSource Track4;
	public float volume;

	void Start () {
		volume = 0.0f;
		Track3.Play();
		Track3.volume = volume;
		Track4.volume = 1.0f;
	}

	void OnTriggerEnter(Collider Gobject)
	{
		if(Gobject.tag == "Player")
		{
			Track3.volume = 1.0f;
			Track4.volume = 0.0f;
		}
	}

	void OnTriggerExit(){
		Track3.volume = 0.0f;
		Track4.volume = 1.0f;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
