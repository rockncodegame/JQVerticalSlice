using UnityEngine;
using System.Collections;

public class CameraFollow2 : MonoBehaviour {
	public GameObject p;
	public Vector3 pPos;
	public float[] zooms;
	public int zone;
	public float camY, camZ;
	public GameObject train1, train2;
	public bool isLocked, playedScratch;
	public AudioSource scratch;
	// Use this for initialization
	void Start () {
		p = GameObject.Find ("Player");
		zone = 0;
		isLocked = false;
		playedScratch = true;
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
		if (train1 != null &&train2 != null){
			switch (zone) {
				case 0:
					train1.SetActive (true);
					train2.SetActive (true);
					break;
				case 1:
					train1.SetActive (true);
					train2.SetActive (true);
					break;
				case 2:
					train1.SetActive (false);
					train2.SetActive (true);
					break;
				case 3:
					train1.SetActive (false);
					train2.SetActive (true);
					break;
				case 4:
					train1.SetActive (false);
					train2.SetActive (false);
					break;
			default:
					break;
			}
		}
		if (!isLocked){
			transform.position = new Vector3 (pPos.x, camY, camZ);
			Enemy_pitch.Start_Sound = true;
			if (!playedScratch){
				scratch.Play();
				playedScratch = true;
			}
		}
		else {
			transform.position = new Vector3 (transform.position.x, transform.position.y, camZ);
			Enemy_pitch.Start_Sound = false;
			if (!playedScratch){
				scratch.Play();
				playedScratch = true;
			}
		}
	}
}
