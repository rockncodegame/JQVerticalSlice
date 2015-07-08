using UnityEngine;
using System.Collections;

public class ZoomZones : MonoBehaviour {
	public GameObject cam;
	public CameraFollow2 camPos;
	public int zoneID;
	// Use this for initialization
	void Start () {
		cam = GameObject.Find ("Main Camera");
		camPos = cam.GetComponent<CameraFollow2> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "Player") {
			camPos.zone = zoneID;
		}
	}
}
