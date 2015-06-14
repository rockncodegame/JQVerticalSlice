using UnityEngine;
using System.Collections;

public class camerashake : MonoBehaviour {
	public float amplitude = 0.1f;
	public float duration = 0.5f;

	private Vector3 initialPosition;
	// Use this for initialization
	void Start () {
		initialPosition = transform.localPosition;
	
		}// Update is called once per frame
	void Update () {
		transform.localPosition = transform.localPosition + Random.insideUnitSphere * amplitude;

	
	}
}
