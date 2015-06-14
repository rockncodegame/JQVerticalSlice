using UnityEngine;
using System.Collections;

public class moveBack : MonoBehaviour
{
	public float distance;
	public Vector3 playerPosition;
	public CharacterController controller;
	private float speed;
	public float velK;


		// Use this for initialization
		void Start ()
		{
		}
	
		// Update is called once per frame
		void Update ()
		{
		GetComponent<SpriteRenderer>().color = Color.red;
		playerPosition = (GameObject.Find ("Player").transform.position);
		distance = Vector3.Distance(playerPosition, transform.position);
		float speed = -5 * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, playerPosition, speed);
		}
}

