using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public GameObject p;
	public bool isLocked, reset, inBuilding;
	public Vector3 pPos, pPosZoom, camInitial;
	public float zoom, startZ;
	// Use this for initialization
	void Start () {
		p = GameObject.Find ("Player");
		isLocked = false;
		reset = false;
		inBuilding = false;
		startZ = transform.position.z;
		zoom = p.transform.position.z - 2;
	}
	
	// Update is called once per frame
	void Update () {
		pPos = new Vector3 (p.transform.position.x, transform.position.y, transform.position.z);
		pPosZoom = new Vector3 (p.transform.position.x, transform.position.y, zoom);
		camInitial = new Vector4 (p.transform.position.x, transform.position.y, startZ);
		if (!isLocked)
			transform.position = pPos;
		if (inBuilding){
			if (transform.position.z != zoom)
				transform.position = Vector3.MoveTowards(transform.position, pPosZoom, .1f);
		}
		else {
			if (transform.position.z != startZ)
				transform.position = Vector3.MoveTowards(transform.position, camInitial, .1f);
		}
	}
}
