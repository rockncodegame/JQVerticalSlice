using UnityEngine;
using System.Collections;

public class GuitarAI : MonoBehaviour
{
	
	public float distance;
	public float health = 5;
	public Vector3 playerPosition;
	public CharacterController controller;
	public float speed;
	
	
	enum States
	{
		Init,
		Retreat,
		Attack,
		Advance,
		Death,
	};
	
	States CurrentState = States.Init;
	
	public GameObject aPlayer; 
	public moveBack moveBackE;
	public Attack attackT;
	public Advance moveCloseE;
	public Strafe strafeE;
	// Use this for initialization
	void Start ()
	{
		
		//grabbing outside scripts and variables
		attackT = GetComponent<Attack>();
		moveBackE = GetComponent<moveBack> ();
		moveCloseE = GetComponent<Advance> ();
		strafeE = GetComponent<Strafe> ();
	}
	
	// Update is called once per frame
	void Update ()
	{ 
		playerPosition = (GameObject.Find ("Player").transform.position);
		distance = Vector3.Distance(playerPosition, transform.position);
		
		if (distance < 2) {
			changeState (States.Retreat);
			
		} else if (distance > 6) {
			changeState (States.Advance);
			
		} else {
			changeState (States.Attack);
		}
		
	}
	
	
	void Retreat(){
		moveBackE.enabled = true;
		moveCloseE.enabled = false;
		attackT.enabled = false;
	}
	void Advance(){
		moveCloseE.enabled = true;
		moveBackE.enabled = false;
		attackT.enabled = false;
	}
	void Init(){
		
	}
	
	void Attack(){
		attackT.enabled = true;
		moveCloseE.enabled = false;
		moveBackE.enabled = false;

	}
	
	void changeState(States newState)
	{
		if (CurrentState == newState) {
			return;
		}
		
		switch(newState)
		{
		case States.Init:
		{
			Init();
			break;
		}
		case States.Retreat:
		{
			Retreat();
			break;
		}
		case States.Advance:
		{
			Advance();
			break;
		}
		case States.Attack:
		{
			Attack();
			break;
		}
		default:
		{ return;
		}
		}
		
	}
	
}