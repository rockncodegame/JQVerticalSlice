using UnityEngine;
using System.Collections;

public class CreditsScroll : MonoBehaviour {

	public GameObject credits;
	public int scrollSpeed;
	public GameObject p;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		credits.transform.position += new Vector3(0, 1, 0) * Input.GetAxis("Horizontal") * scrollSpeed * Time.deltaTime;
	}
}
