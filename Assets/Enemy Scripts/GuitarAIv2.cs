using UnityEngine;
using System.Collections;

public class GuitarAIv2 : MonoBehaviour
{
	
	public GameObject Bullet;
	public GameObject p;
	public Vector3 playerPosition;
	public float distance;
	public float stun;
	public float speed;

	Animator anim;
	int AttackHash = Animator.StringToHash("Attack");
	public float delay;
	enum States
	{
		Idle,
		Attack,
		Movement,
		Death,
	};

	// Use this for initialization
	States CurrentState = States.Movement;

	void Start (){
		//grabbing outside scripts and variables
		GetComponent<EnemyController>().health = 2;
		anim = GetComponent<Animator> ();
		speed = 3.5f * Time.deltaTime;
		p = GameObject.Find ("Player");

	}
	
	// Update is called once per frame
	void Update (){ 
		if (p != null) {
			//is stunned
			stun = GetComponent<EnemyController>().shockTime;
			if (stun <= Time.time) {
				//grab where player is
				playerPosition = p.transform.position;
				distance = Vector3.Distance(playerPosition, transform.position);

				if(delay <= Time.time & CurrentState == States.Idle & distance <10){
					changeState(States.Attack);
				}
				if(delay <= Time.time & CurrentState == States.Idle){
					changeState(States.Movement);
				}
					}
			}

		if (CurrentState == States.Movement) {
			Movement ();
		}
		if (CurrentState == States.Idle) {
			Idle ();
		}
		if (CurrentState == States.Attack) {
			Attack ();
		}
		
	}
	
	

	void Movement(){
		if (distance >= 8){
			transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed);
		}
		//return to idle
		if (distance <8)
		{
			changeState(States.Idle);
			delay = Time.time + 1.5f;
		}
	}
	void Idle(){

	}
	
	void Attack(){
				//animate
				anim.SetTrigger (AttackHash);
				//form bullet
				Instantiate(Bullet, transform.position, transform.rotation);
				Bullet.rigidbody.AddForce(Bullet.transform.forward * 2);
		changeState(States.Idle);
		delay = Time.time + 2;
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
		case States.Movement:
		{
			Movement();
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