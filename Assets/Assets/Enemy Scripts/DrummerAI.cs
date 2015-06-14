using UnityEngine;
using System.Collections;

public class DrummerAI : MonoBehaviour
{
	//creating variables
	public float attackTime;
	public float distance;
	private float speed;
	public Vector3 playerPosition;
	public Vector3 spawnPoint;
	public float beat;
	public Sprite DrummerSprite;
	public string cState;

	//starting state system
	enum States
	{
		Idle,
		Retreat,
		Attack,
		Advance,
		Death,
	};
	States CurrentState = States.Idle;

	//creating holders for outside scripts
	public GameObject Player; 
	public moveBack moveBackE;
	public Attack attackT;
	public Advance moveCloseE;

	// Use this for initialization
	void Start ()
	{
		//grabbing outside scripts and variables
		spawnPoint = transform.position;
		GetComponent<EnemyController>().health = 5;
		attackT = GetComponent<Attack>();
		moveBackE = GetComponent<moveBack> ();
		moveCloseE = GetComponent<Advance> ();
		//setting sprite
		//GetComponent<SpriteRenderer>().sprite = DrummerSprite;
		attackTime = 0;
		InvokeRepeating ("BeatTime", 2,1);

	}
	
	// Update is called once per frame
	void Update ()
	{ 	

		// getting player position and distance between
		playerPosition = (GameObject.Find ("Player").transform.position);
		distance = Vector3.Distance (playerPosition, transform.position);
		//switching states if  conditions met
		if((beat == 4 || beat == 12) && distance > 2){
			changeState(States.Advance);
			attackTime = 0;
		}
		 if (attackTime > 150){
			changeState(States.Retreat);
		}
		if (distance <= 3 && attackTime ==0) {
			changeState(States.Attack);
		}
		if (CurrentState == States.Advance)
			{
				Advance();
			}

			else if (CurrentState == States.Retreat)
			{
				Retreat();
			}
			else if (CurrentState == States.Attack)
			{
				Attack();
			}
			
			else if (CurrentState == States.Idle)
			{
				Idle();
			}
		
	}

	

	void BeatTime(){
		//self kept beat
		if(beat == 16){
			beat = 0;
			}
		beat++;
			}


	
	void Retreat(){
		//turn off and on scripts
		//moveBackE.enabled = true;
		moveCloseE.enabled = false;
		attackT.enabled = false;
		speed = 3 * Time.deltaTime;
		//moves back to spawn point
		if (transform.position.x >= (spawnPoint.x -1) && transform.position.x <= (spawnPoint.x +1)){ 
			changeState(States.Idle);
		}
		transform.position = Vector3.MoveTowards (transform.position, spawnPoint, speed);
		}

	void Advance(){
		//turn off and on scripts
		//moveCloseE.enabled = true;
		moveBackE.enabled = false;
		attackT.enabled = false;
		speed = 7 * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, playerPosition, speed);
		if (distance <= 3) {
			changeState(States.Attack);
		}
	}

	void Idle(){
		//turn off and on scripts
		moveCloseE.enabled = false;
		moveBackE.enabled = false;
		attackT.enabled = false; 
		attackTime = 0;
		speed = 0;
	}
	
	void Attack(){
		//turn off and on scripts
		attackT.enabled = true;
		moveCloseE.enabled = false;
		moveBackE.enabled = false;
		attackTime++;
	}
	
	void changeState(States newState)
	{	//states changer machine
		if (CurrentState == newState) {
			return;
		}
		
		switch(newState)
		{
		case States.Idle:
		{
			Idle();
			CurrentState = newState;
			break;
		}
		case States.Retreat:
		{
			Retreat();
			CurrentState = newState;
			break;
		}
		case States.Advance:
		{

			Advance();
			CurrentState = newState;
			break;
		}
		case States.Attack:
		{
			Attack();
			CurrentState = newState;
			break;
		}
		default:
		{ return;
		}
		}
		
	}
	
}