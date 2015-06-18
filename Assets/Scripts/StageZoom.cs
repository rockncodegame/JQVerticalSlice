using UnityEngine;
using System.Collections;

public class StageZoom : MonoBehaviour {
	public GameObject cam;
	public CameraFollow cFollow;
	// Use this for initialization
	void Start () {
		cam = GameObject.Find ("Main Camera");
		cFollow = cam.GetComponent<CameraFollow> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "Player")
			cFollow.inBuilding = true;
	}
	void OnTriggerExit(Collider c) {
		if (c.gameObject.tag == "Player")
			cFollow.inBuilding = false;
	}
}
