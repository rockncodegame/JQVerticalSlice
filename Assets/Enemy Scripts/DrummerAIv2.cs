using UnityEngine;
using System.Collections;

public class DrummerAIv2 : MonoBehaviour
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
	
	public Vector3 nextTarget;
	Vector3 crossR;
	Vector3 crossL;
	
	public float nextMove;
	public float idleTime;
	public GameObject Bullet;
	public GameObject p;
	//attack variables
	double nextBlast=0;
	double delay = 2;
	double attacked = 0;
	
	Animator anim;
	int AttackHash = Animator.StringToHash("Attack");
	
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
	// Use this for initialization
	void Start ()
	{
		
		//grabbing outside scripts and variables
		spawnPoint = transform.position;
		spawnPoint.y = playerPosition.y;
		anim = GetComponent<Animator> ();
		GetComponent<EnemyController>().health = 5;
		p = GameObject.Find ("Player");
		attackTime = 0;
		
	}
	
	// Update is called once per frame
	void Update ()
	{ 	
		
		// getting player position and distance between
		if (p != null) {
			playerPosition = p.transform.position;
			distance = Vector3.Distance (playerPosition, transform.position);
			//switching states if  conditions met
			if(distance < 6 && Time.time >= delay){
				changeState(States.Attack);
				delay = Time.time +3;
			}
			else if(distance >= 6 ){
				changeState(States.Advance);

			}
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
	
	
	

	
	void Retreat(){
		speed = 4 * Time.deltaTime;
		idleTime = Time.time + 2;
		transform.position = Vector3.MoveTowards (transform.position, spawnPoint, speed);
		//moves back to spawn point
		if (transform.position.x >= (spawnPoint.x -2) && transform.position.x <= (spawnPoint.x +2)){ 
			changeState(States.Idle);
			
		}
	}
	
	void Advance(){
		speed = 1.5f * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, playerPosition, speed);
		
	}
	
	void Idle(){
		attacked = 0;
	}
	
	void Attack(){
		//turn off and on scripts
		if(Time.time > nextBlast){
			//animate
			anim.SetTrigger (AttackHash);
			// create bullets

			crossL.x =transform.position.x -1;
			crossL.y =transform.position.y +1;
			crossL.z = transform.position.z;
			Instantiate(Bullet, crossL, transform.rotation);
			idleTime = Time.time + 3;
			changeState(States.Idle);
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