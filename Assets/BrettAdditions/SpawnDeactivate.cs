using UnityEngine;
using System.Collections;

public class SpawnDeactivate : MonoBehaviour {

	public VisSpawnPrefabTrigger spawner;
	public GameObject p;
	public GameObject spawnerPosition;
	public float deactivateDistance;
	
	// Use this for initialization
	void Start () {
		spawner.GetComponentInParent<VisSpawnPrefabTrigger> ();
		p = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if ((p.transform.position.x - spawnerPosition.transform.position.x > deactivateDistance) || (p.transform.position.x - spawnerPosition.transform.position.x < (deactivateDistance * -1))) {
			spawner.enabled = false;
		} else {
			spawner.enabled = true;
		}
	}
}
