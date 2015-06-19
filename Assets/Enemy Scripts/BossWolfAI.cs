using UnityEngine;
using System.Collections;

public class BossWolfAI : MonoBehaviour {
	static public float phase;
	public bool Reset;
	public float BossHP;
	public Vector3 Pedastal;
	public Vector3 MoveTarget;
	public Vector3 playerPosition;
	public float distance;
	public float distanceTarget;
	private float speed;
	private float moved;
	private float beat;
	//bullet variables
	public Rigidbody projectile;
	public GameObject Bullet;

	double nextBlast=0;
	double delay = 2;
	double attacked = 0;

	enum States
	{
		Idle,
		PS1Movement,
		PS2Movement,
		PS3Movement,
		PS1Attack,
		PS2Attack,
		PS3Attack,
		ResetMove,
		Death,
	};
	States CurrentState = States.Idle;


	// Use this for initialization
	void Start () {
		InvokeRepeating ("BeatTime", 2,1);
		Reset = false;
		GetComponent<BossEnemyController>().health = 10;
		BossHP = GetComponent<BossEnemyController>().health;
		phase++;
		speed = 3 * Time.deltaTime;
		playerPosition = (GameObject.Find ("Player").transform.position);
		MoveTarget = playerPosition;
		MoveTarget.x = (MoveTarget.x + Random.Range(-2,2));
		MoveTarget.z = (MoveTarget.z + Random.Range(-2,2));
	}
	
	// Update is called once per frame
	void Update () {
		//distance calcs
		playerPosition = (GameObject.Find ("Player").transform.position);
		distance = Vector3.Distance (playerPosition, transform.position);
		distanceTarget = Vector3.Distance (MoveTarget, transform.position);

		//change phase on health hit 0
		if (BossHP < 1 && Reset == true) {
			NextPhase();
		}
		if(beat == 2 && CurrentState == States.Idle){
			changeState(States.PS1Movement);
			
		}

		// matching behavior to state
		if (CurrentState == States.ResetMove)
		{
			ResetMove();
		}
		if (CurrentState == States.Idle)
		{
			Idle();
		}
		if (CurrentState == States.PS1Movement)
		{
			Ps1Movement();
		}
		if (CurrentState == States.PS1Attack)
		{
			Ps1Attack();
		}

	}
	


	void Idle(){

	}
	//update the phase
	void NextPhase(){
		phase++;
		GetComponent<BossEnemyController>().health = 10;

	}
	//phase 1 traits
	void Ps1Movement(){
		transform.position = Vector3.MoveTowards (transform.position, playerPosition, speed);

		if (MoveTarget == transform.position || distance <=2) {

			MoveTarget.x = (MoveTarget.x +2);
			MoveTarget.z = (MoveTarget.z + Random.Range(-2,2));

			moved++;
		}

		if (distance <= 2 && moved >=3) {
			changeState(States.PS1Attack);
			moved = 0;

		}
	}

	void Ps1Attack(){
		if(Time.time > nextBlast){
			// create bullet
			nextBlast = Time.time + delay;
			Instantiate(Bullet, transform.position, transform.rotation);
			Bullet.rigidbody.AddForce(Bullet.transform.forward * 2);
			attacked++;
		}
		if (attacked > 3) {
			changeState(States.Idle);
		}
		//voice bunny
		//gdc audio
	}

	//phase 2 traits
	void Ps2Movement(){
		
	}
	
	void Ps2Attack(){
		
	}

	//phase 3 traits
	void Ps3Movement(){
		
	}
	
	void Ps3Attack(){
		
	}
	// move back to pedastal  
	void ResetMove(){

		if (transform.position == Pedastal){
			Reset = true;
		}
	}

	void BeatTime(){
		//self kept beat
		if(beat == 16){
			beat = 0;
		}
		beat++;
	}

	void Death(){

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
		case States.Death:
		{
			Death();
			CurrentState = newState;
			break;
		}
		case States.PS1Movement:
		{
			
			Ps1Movement();
			CurrentState = newState;
			break;
		}
		case States.PS1Attack:
		{
			Ps1Attack();
			CurrentState = newState;
			break;
		}

		case States.PS2Movement:
		{
			
			Ps2Movement();
			CurrentState = newState;
			break;
		}
		case States.PS2Attack:
		{
			Ps2Attack();
			CurrentState = newState;
			break;
		}

		case States.PS3Movement:
		{
			
			Ps3Movement();
			CurrentState = newState;
			break;
		}
		case States.PS3Attack:
		{
			Ps3Attack();
			CurrentState = newState;
			break;
		}
		case States.ResetMove:
		{
			ResetMove();
			CurrentState = newState;
			break;
		}

		default:
		{ 
			return;
		}
		}
		
	}
}
