using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
	public Vector3 ogPosi;
	public float distance;
	public float health = 5;
	public Vector3 playerPosition;
	Vector3 movement = Vector3.zero;
	public CharacterController controller;
		// Use this for initialization
		void Start ()
	{ controller = GetComponent<CharacterController> ();
		ogPosi = transform.position;
		}
	
		// Update is called once per frame
		void Update ()
	{	
		if (controller.isGrounded == false)
			movement.y += Physics.gravity.y * Time.deltaTime;

		if (distance <= 1) {
			health = health -1;
		} 
		float speed = 1 * Time.deltaTime;
		playerPosition = (GameObject.Find ("Player").transform.position);
		distance = Vector3.Distance(playerPosition, transform.position);
			if (distance < 3) {
						speed = -3 * Time.deltaTime;
						transform.position = Vector3.MoveTowards (transform.position, playerPosition, speed); 
						//transform.position = Vector3.MoveTowards (transform.position, ogPosi, speed);

			GetComponent<SpriteRenderer>().color = Color.green;

				} else if (distance > 5) {
						speed = 3 * Time.deltaTime;
						;
						transform.position = Vector3.MoveTowards (transform.position, playerPosition, speed); 
			GetComponent<SpriteRenderer>().color = Color.gray;
						health = 3;
				} else {
			GetComponent<SpriteRenderer>().color = Color.red;
				}
		controller.Move (movement * Time.deltaTime);
		}

}

