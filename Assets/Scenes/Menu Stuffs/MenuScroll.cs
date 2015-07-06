using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScroll : MonoBehaviour {
	public Transform[] positions;
	public int pos;
	public bool isMoving;
	public Button btn;
	public int min, max;
	// Use this for initialization
	void Start () {
		btn = GetComponent<Button> ();
		isMoving = false;
		min = 1;
		max = 4;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftArrow) && !isMoving){
			MoveBack ();
		}

		if (Input.GetKey (KeyCode.RightArrow) && !isMoving){
			MoveForward();
		}

		if (isMoving) {
			btn.enabled = false;
		}
		else if (pos != 1){
			btn.enabled = false;
		}
		else {
			btn.enabled = true;
		}

		if (transform.position == positions [pos-1].position) {
			isMoving = false;
			ChangeOrder ();
		}
		else {
			transform.position = Vector3.MoveTowards (transform.position, positions[pos-1].position, 5);
			transform.localScale = Vector3.MoveTowards (transform.localScale, positions[pos-1].localScale, .0075f);
		}
	}
	
	void MoveForward(){
		pos++;
		if (pos > max)
			pos = min;
		isMoving = true;
		StartCoroutine ("ChangeOrder");
	}
	void MoveBack(){
		pos--;
		if (pos < min)
			pos = max;
		isMoving = true;
		StartCoroutine ("ChangeOrder");
	}

	IEnumerator ChangeOrder(){
		yield return new WaitForSeconds(.3f);
		switch (pos) {
		case 1:
			transform.SetSiblingIndex (3);
			btn.enabled = true;
			break;
		case 2:
			transform.SetSiblingIndex (2);
			btn.enabled = false;
			break;
		case 3:
			transform.SetSiblingIndex (0);
			btn.enabled = false;
			break;
		case 4:
			transform.SetSiblingIndex (1);
			btn.enabled = false;
			break;
		default:
			break;
		}
	}
}
