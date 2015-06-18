using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public GameObject p;
	public bool isLocked, reset;
	public Vector3 pPos;
	// Use this for initialization
	void Start () {
		p = GameObject.Find ("Player");
		isLocked = false;
		reset = false;

	}
	
	// Update is called once per frame
	void Update () {
		pPos = new Vector3 (p.transform.position.x, transform.position.y, transform.position.z);
		if (!isLocked)
			transform.position = pPos;;
	}
}
