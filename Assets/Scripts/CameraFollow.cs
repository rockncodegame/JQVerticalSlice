using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public GameObject p, bg;
	public bool isLocked, reset, inBuilding;
	public Vector3 pPos, pPosZoom, bgPosZoom, bgInitial, camInitial;
	public float zoomCamera, zoomBGManager, startZCamera, startZBGManager;
	// Use this for initialization
	void Start () {
		p = GameObject.Find ("Player");
		bg = GameObject.FindGameObjectWithTag ("BGManager");
		isLocked = false;
		reset = false;
		inBuilding = false;
		startZCamera = transform.position.z;
		startZBGManager = bg.transform.position.z;
		zoomCamera = p.transform.position.z - 2;
		zoomBGManager = p.transform.position.z - 15;
	}
	
	// Update is called once per frame
	void Update () {
		pPos = new Vector3 (p.transform.position.x, transform.position.y, transform.position.z);
		pPosZoom = new Vector3 (p.transform.position.x, transform.position.y, zoomCamera);
		camInitial = new Vector3 (p.transform.position.x, transform.position.y, startZCamera);
		bgPosZoom = new Vector3 (p.transform.position.x, bg.transform.position.y, zoomBGManager);
		bgInitial = new Vector3 (p.transform.position.x, bg.transform.position.y, startZBGManager);

		if (!isLocked)
			transform.position = pPos;
		if (inBuilding){
			if (transform.position.z != zoomCamera)
				transform.position = Vector3.MoveTowards(transform.position, pPosZoom, .1f);
			if (bg.transform.position.z != zoomBGManager)
				bg.transform.position = Vector3.MoveTowards(bg.transform.position, bgPosZoom, .17f);
		}
		else {
			if (transform.position.z != startZCamera)
				transform.position = Vector3.MoveTowards(transform.position, camInitial, .1f);
			if (bg.transform.position.z != startZBGManager)
				bg.transform.position = Vector3.MoveTowards(bg.transform.position, bgInitial, .17f);
		}
	}
}
