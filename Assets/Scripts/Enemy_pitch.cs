using UnityEngine;
using System.Collections;

public class Enemy_pitch : MonoBehaviour {
	public int startingPitch = 1;
	static float timeToDecrease = 1.1f;
	public AudioSource audio;
	public static bool Start_Sound;


	// Use this for initialization

	void Start () {
		//audio = GetComponent<AudioSource>();
		Start_Sound = true;
	}

	/*void OnTriggerEnter(Collider other){
		audio.pitch = timeToDecrease;
	}

	void OnTriggerExit(Collider other){
		audio.pitch = startingPitch;
	}*/

	// Update is called once per frame
	void Update () {
		if (Start_Sound == false)
			audio.pitch = timeToDecrease;
		else
			audio.pitch = startingPitch;
	}
}
