using UnityEngine;
using System.Collections;

public class SingerAI : MonoBehaviour
{	
	public float distance;
	public Vector3 playerPosition;
	public float speed;
	public Sprite SingerSprite;
	public float beat;
	public int wait;
	public Vector3 NewPosition;
	public int strafeMod;
	public PlayerAttack pAttack;
	public GameObject Bullet;
	public GameObject p;
	public Vector3 attackPoint;
	//attack variables
	double nextMove;
	double nextBlast=0;
	double delay = 1;
	double attacked = 0;
	public float stun;
	Animator anim;
	int AttackHash = Animator.StringToHash("Attack");
	
	enum States
	{
		Init,
		Idle,
		Retreat,
		Attack,
		Advance,
		Death,
	};
	
	private States CurrentState = States.Init;
	
	public GameObject Player; 
	
	
	// Use this for initialization
	void Start ()
	{
		//grabbing outside scripts and variables
		GetComponent<EnemyController>().health = 1;
		pAttack = GetComponent<PlayerAttack> ();
		strafeMod = Random.Range(1,2);
		anim = GetComponent<Animator> ();
		InvokeRepeating ("BeatTime", 2,1);
		speed = 3 * Time.deltaTime;
		
		p = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{ // getting player position and distance between
		stun = GetComponent<EnemyController>().shockTime;
		playerPosition = p.transform.position;
		distance = Vector3.Distance (playerPosition, transform.position);
		if (stun <= Time.time) {
			
			if (distance < 5 && nextMove <= Time.time) {
				changeState (States.Retreat);
				nextMove = Time.time + 4;
			} else if (distance > 7) {
				changeState (States.Advance);
			} else if (beat >= 4 && beat <= 6) {
				attackPoint = new Vector3(transform.position.x, transform.position.y, playerPosition.z);
				changeState (States.Attack);
				//Strafe();
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
		}
	}
	void Strafe(){
		
		speed = 2 * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, NewPosition, speed);
		
		
		if (strafeMod > 0 && transform.position.z >= NewPosition.z) {
			strafeMod = -strafeMod;
			NewPosition = transform.position;
			NewPosition.z = (transform.position.z + strafeMod);
			//NewPosition.y = (transform.position.y + strafeMod);
		}
		if (strafeMod < 0 && transform.position.z <= NewPosition.z) {
			strafeMod = -strafeMod;
			NewPosition = transform.position;
			NewPosition.z = (transform.position.z + strafeMod);
			//NewPosition.y = (transform.position.y + strafeMod);
		}
	}
	
	
	void WaitTimer(){
		wait = 6000;
		while (wait > 0) {
			wait--;
		}
	}
	
	
	void Retreat(){
		speed = -5 * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, playerPosition, speed);
		if (distance >= 6) {
			changeState (States.Idle);
		}
	}
	
	void Advance(){
		speed = 2 * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, playerPosition, speed);
		
		if (distance <= 6 ) {
			changeState (States.Idle);
		}
	}
	
	void Init(){
		
	}
	
	void Attack(){
		//move to same z as player
		
		speed = 10 * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, attackPoint, speed);
		
		if(Time.time > nextBlast){
			//animate
			anim.SetTrigger (AttackHash);
			// create bullet
			nextBlast = Time.time + delay;
			
			Instantiate(Bullet, transform.position, transform.rotation);
			attacked++;
		}
		speed = 3 * Time.deltaTime;
		if (attacked > 1) {
			changeState(States.Idle);
		}
	}
	
	void BeatTime(){
		//self kept beat
		if(beat == 16){
			beat = 0;
		}
		beat++;
	}
	
	void Idle(){
		attacked = 0;
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
			CurrentState = newState;
			break;
		}
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