using UnityEngine;
using System.Collections;

public class BounceActivator : MonoBehaviour {

	public GameObject originPoint;
	public GameObject bounceContainer;
	public VisGameObjectPropertyTrigger bounceScript;
	public GameObject p;
	private float stopPointLeft;
	private float stopPointRight;
	public float buffer;

	// Use this for initialization
	void Start () {
		bounceScript = bounceContainer.GetComponent<VisGameObjectPropertyTrigger> ();
		stopPointLeft = originPoint.transform.localPosition.x - buffer;
		stopPointRight = originPoint.transform.localPosition.x + buffer;
	}
	
	// Update is called once per frame
	void Update () {
		if (p.transform.localPosition.x >stopPointLeft && p.transform.localPosition.x < stopPointRight) {
			bounceScript.enabled = true;
		}
		else {
			bounceScript.enabled = false;
		}
	}
}
