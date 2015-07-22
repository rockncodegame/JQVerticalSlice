using UnityEngine;
using System.Collections;

public class SingerAIv2 : MonoBehaviour
{	
	public float distance;
	public Vector3 playerPosition;
	public float speed;
	public float delay;
	public float retreatTime;
	public GameObject Bullet;
	public GameObject p;
	public Vector3 attackPoint;
	//attack variables
	double nextMove;
	double nextBlast;
	double attacked = 0;
	public float stun;

	//set animator
	Animator anim;
	int AttackHash = Animator.StringToHash("Attack");
	
	enum States
	{
		Idle,
		Retreat,
		Attack,
		Advance,
		Death,
	};
	
	private States CurrentState = States.Advance;

	
	// Use this for initialization
	void Start ()
	{
		//grabbing outside scripts and variables
		GetComponent<EnemyController>().health = 5;
		anim = GetComponent<Animator> ();
		speed = 4 * Time.deltaTime;
		
		p = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{ // getting player position and distance between
		stun = GetComponent<EnemyController>().shockTime;
		playerPosition = p.transform.position;
		distance = Vector3.Distance (playerPosition, transform.position);
		if (stun <= Time.time) {
			
			if (distance < 7 && delay <= Time.time) {
				changeState (States.Retreat);
				delay = Time.time + 5;
				retreatTime = Time.time +2;
			} else if (distance >= 12 && delay <= Time.time) {
				changeState (States.Advance);
				delay = Time.time + 2;
			} else if (delay <= Time.time && CurrentState == States.Idle) {
				attackPoint = new Vector3(transform.position.x, transform.position.y, playerPosition.z);
				changeState (States.Attack);

			}
			if (CurrentState == States.Advance) {
				Advance ();
			}
			if (CurrentState == States.Attack) {
				Attack ();
			}
			if (CurrentState == States.Retreat) {
				Retreat ();
			}
			if (CurrentState == States.Idle) {
				Idle ();
			}
		}
	}
	
	

	
	
	void Retreat(){
		attacked =0;
		speed = -9 * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, playerPosition, speed);
		if (distance >= 16 || retreatTime <= Time.time) {
			changeState (States.Idle);
		}
	}
	
	void Advance(){
		attacked =0;
		speed = 4 * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, playerPosition, speed);
		
		if (distance <= 9 ) {
			changeState (States.Idle);
		}
	}
	

	
	void Attack(){
		//move to same z as player
		
		speed = 3 * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, attackPoint, speed);
		if (transform.position == attackPoint && attacked <1)
		{
			if(Time.time > delay){
			//animate
			anim.SetTrigger (AttackHash);
			// create bullet
			delay = Time.time + 3;
			
			Instantiate(Bullet, transform.position, transform.rotation);
			
				attacked++;
			}
		} 

		speed = 3 * Time.deltaTime;
		//goto to Retreat
		if (Time.time >=delay && attacked >0) {
			delay = Time.time + 5;
			retreatTime = Time.time +2;
			changeState(States.Retreat);
			attacked =0;
		}
	}
	

	
	void Idle(){
	}
	
	void changeState(States newState)
	{
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