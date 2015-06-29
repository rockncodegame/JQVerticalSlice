using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public GameObject p, bg;
	public bool isLocked, reset, inBuilding, zoomedIn;
	public Vector3 pPos, pPosZoom, bgPosZoom, bgInitial, camInitial;
	public float zoomCamera, startZCamera, startYCamera, zoomYCamera;
	// Use this for initialization
	void Start () {
		p = GameObject.Find ("Player");
		bg = GameObject.FindGameObjectWithTag ("BGManager");
		isLocked = false;
		reset = false;
		inBuilding = false;
		zoomedIn = false;
		startZCamera = transform.position.z;
		startYCamera = transform.position.y;
		zoomCamera = p.transform.position.z - 5;
		zoomYCamera = p.transform.position.y;
		bgInitial = bg.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		pPos = new Vector3 (p.transform.position.x, transform.position.y, transform.position.z);
		pPosZoom = new Vector3 (p.transform.position.x, zoomYCamera, zoomCamera);
		camInitial = new Vector3 (p.transform.position.x, startYCamera, startZCamera);
		bg.transform.position = bgInitial;

		if (!isLocked)
			transform.position = pPos;

		if (inBuilding){
			if (transform.position.z != zoomCamera && transform.position.y != zoomYCamera)
				transform.position = Vector3.MoveTowards(transform.position, pPosZoom, .3f);
			if (transform.position.z == zoomCamera && transform.position.y == zoomYCamera)
				zoomedIn = true;
			if (zoomedIn)
				transform.position = new Vector3 (p.transform.position.x, p.transform.position.y, zoomCamera);
		}
		else {
			if (transform.position.z != startZCamera)
				transform.position = Vector3.MoveTowards(transform.position, camInitial, .3f);
			else 
				zoomedIn = false;
		}
	}
}
