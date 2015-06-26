using UnityEngine;
using System.Collections;

public class NoteDestroy : MonoBehaviour {
	public ParticleSystem particles;
	// Use this for initialization
	void Start () {
		particles = gameObject.GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (particles.isPlaying)
			return;
		else
			Destroy (gameObject);
	}
}
