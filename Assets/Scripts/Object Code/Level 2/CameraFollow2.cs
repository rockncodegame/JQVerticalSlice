using UnityEngine;
using System.Collections;

public class CameraFollow2 : MonoBehaviour {
	public GameObject p;
	public Vector3 pPos;
	public float[] zooms;
	public int zone;
	public float camY, camZ;
	// Use this for initialization
	void Start () {
		p = GameObject.Find ("Player");
		zone = 0;
	}
	
	// Update is called once per frame
	void Update () {
		pPos = p.transform.position;
		camY = pPos.y + 6f;
		camZ = transform.position.z;
		if (camZ == zooms[zone])
			camZ = zooms[zone];
		else {
			if (camZ < zooms [zone])
				camZ += .1f;
			if (camZ > zooms [zone])
				camZ -= .1f;
		}

		transform.position = new Vector3 (pPos.x, camY, camZ);
	}
}
