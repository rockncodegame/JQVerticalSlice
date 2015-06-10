using UnityEngine;
using System.Collections;

public class DrummerAI : MonoBehaviour
{
	//creating variables
	public float attackTime;
	public float distance;
	private float speed;
	public Vector3 playerPosition;
	private States state; 
	private States CurrentState;
	private Vector3 spawnPoint;
	public float beat;
	public Sprite DrummerSprite;

	//starting state system
	enum States
	{
		Idle,
		Retreat,
		Attack,
		Advance,
		Death,
	};
	

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
	{ 	//once  back to start idle and reset and idle
		if (transform.position.x >= (spawnPoint.x - 0.5) && transform.position.x <= (spawnPoint.x + 0.5)) {
			attackTime = 0;
			CurrentState = States.Idle;
		}
		// getting player position and distance between
		playerPosition = (GameObject.Find ("Player").transform.position);
		distance = Vector3.Distance (playerPosition, transform.position);
		//switching states if  conditions met
		if(beat == 4){
			changeState (States.Advance);
		}
		else if (distance <= 2 && attackTime < 200) {
			attackTime++;
			changeState (States.Attack);
		} 
		else if (attackTime >= 200) {
			changeState (States.Retreat);
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
		float speed = 2 * Time.deltaTime;
		//moves back to spawn point
		transform.position = Vector3.MoveTowards (transform.position, spawnPoint, speed);
		}

	void Advance(){
		//turn off and on scripts
		moveCloseE.enabled = true;
		moveBackE.enabled = false;
		attackT.enabled = false;
		if (distance <= 2) {
			changeState (States.Attack);
			Attack();
		} 
	}

	void Idle(){
		//turn off and on scripts
		moveCloseE.enabled = false;
		moveBackE.enabled = false;
		attackT.enabled = false; 
	}
	
	void Attack(){
		//turn off and on scripts
		attackT.enabled = true;
		moveCloseE.enabled = false;
		moveBackE.enabled = false;
		attackTime++;
		if (attackTime >= 200) {
			changeState (States.Retreat);
		}
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