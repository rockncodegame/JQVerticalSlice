using UnityEngine;
using System.Collections;

public class Enemy_pitch : MonoBehaviour {
	public int startingPitch = 1;
	public float timeToDecrease = -1;
	public AudioSource audio;
	// Use this for initialization

	void Start () {
		//audio = GetComponent<AudioSource>();
		audio.pitch = startingPitch;
	}

	void OnTriggerEnter(Collider other){
		audio.pitch = timeToDecrease;
	}

	void OnTriggerExit(Collider other){
		audio.pitch = startingPitch;
	}

	// Update is called once per frame
	void Update () {
		//if (audio.pitch > 0)
		//	audio.pitch -= Time.deltaTime * startingPitch / timeToDecrease;
	}
}
