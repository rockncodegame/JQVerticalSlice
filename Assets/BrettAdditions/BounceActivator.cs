using UnityEngine;
using System.Collections;

public class BounceActivator : MonoBehaviour {
	
	//necessary variable declaration
	public GameObject originPoint;
	public GameObject bounceContainer;
	public VisGameObjectPropertyTrigger bounceScript;
	public GameObject camera;
	public CameraFollow cameraScript;
	public GameObject p;
	private float stopPointLeft;
	private float stopPointRight;
	private float zoomStopPointLeft;
	private float zoomStopPointRight;
	private float buildingStopPoingLeft;
	private float buildingStopPointRight;
	public float buffer;
	public float zoomBuffer;
	public float buildingBuffer;
	
	// Finding necessary scripts and setting base variables
	void Start () {
		bounceScript = bounceContainer.GetComponent<VisGameObjectPropertyTrigger> ();
		cameraScript = camera.GetComponent<CameraFollow> ();
		stopPointLeft = originPoint.transform.localPosition.x - buffer;
		stopPointRight = originPoint.transform.localPosition.x + buffer;
		zoomStopPointLeft = originPoint.transform.localPosition.x - zoomBuffer;
		zoomStopPointRight = originPoint.transform.localPosition.x + zoomBuffer;
		buildingStopPoingLeft = originPoint.transform.localPosition.x - buildingBuffer;
		buildingStopPointRight = originPoint.transform.localPosition.x + buildingBuffer;
	}
	
	// Update is called once per frame
	void Update () {
		//check to see if player is on top of the building
		if (p.transform.localPosition.y > 20 && p.transform.localPosition.x > buildingStopPoingLeft && p.transform.localPosition.x < buildingStopPointRight) {
			bounceScript.enabled = false;
		}
		else{
			//check to see if building is visable while zoomed in
			if (cameraScript.zoomedIn) {
				if(p.transform.localPosition.x > zoomStopPointLeft && p.transform.localPosition.x < zoomStopPointRight){
					bounceScript.enabled = true;
				}
				else{
					bounceScript.enabled = false;
				}
			}
			else {
				//check to see if visible regularly
				if (p.transform.localPosition.x > stopPointLeft && p.transform.localPosition.x < stopPointRight) {
					bounceScript.enabled = true;
				}
				else {
					bounceScript.enabled = false;
				}
			}
		}
	}
}
