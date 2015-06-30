using UnityEngine;
using System.Collections;

public class BossWolfAI : MonoBehaviour {
	public float phase;
	public bool Reset;
	public bool NoEnemies;
	public bool MinionStart;
	public BossEnemyController BossCon;

	// boss spawner
	public GameObject BossES;
	public BossEnemyZone ESZone;
	public Vector3 Pedastal;
	public Vector3 MoveTarget;
	public Vector3 playerPosition;
	public float distance;
	public float distanceTarget;
	public float BossHP;
	private float speed;
	private float moved;
	public float beat;
	//bullet variables
	public Rigidbody projectile;
	public GameObject Bullet;
	//for moving around player
	public float Xvalue;
	public float Yvalue;
	public Vector3 MoveTarget2;

	public Vector3 bullet1;
	public Vector3 bullet2;
	public Vector3 bullet3;
	public Vector3 bullet4;

	//animation
	Animator anim;
	int AttackHash = Animator.StringToHash("Attack");
	int RoarHash = Animator.StringToHash("Roar");
	int DeathHash = Animator.StringToHash("Death");

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
		phase++;
		BossCon = GetComponent<BossEnemyController> ();
		//looking at spawner
		BossES = GameObject.FindWithTag ("BossZone");
		ESZone = BossES.GetComponent<BossEnemyZone> ();
		NoEnemies = true;
		anim = GetComponent<Animator> ();

		speed = 9 * Time.deltaTime;
		playerPosition = (GameObject.Find ("Player").transform.position);
		MoveTarget = playerPosition;
		//pedastal 
		Pedastal = (GameObject.FindWithTag ("Pedastal").transform.position);

	}
	
	// Update is called once per frame
	void Update () {
		Pedastal = (GameObject.FindWithTag ("Pedastal").transform.position);
		//can be changed as phases increase
		if (phase == 3 && BossHP <=0){
			anim.SetTrigger (DeathHash);
			Destroy (gameObject, 1);
		}

		//update health
		BossHP = BossCon.health;
		if (BossHP <1 && phase <2){

			changeState(States.ResetMove);
			rigidbody.useGravity = false;

		}

		//distance calcs
		playerPosition = (GameObject.Find ("Player").transform.position);
		distance = Vector3.Distance (playerPosition, transform.position);
		distanceTarget = Vector3.Distance (MoveTarget, transform.position);

		if (ESZone.e.Count == 0 && ESZone.wave >= ESZone.numWaves) {
			NoEnemies = true;
				}

		//change phase on health hit 0
		if (BossHP < 1 && Reset == true && NoEnemies == true) {
			NextPhase();

			anim.SetTrigger (RoarHash);

		}

		if(beat == 2 && CurrentState == States.Idle && phase == 1){
			changeState(States.PS1Movement);
			
		}
		if(beat == 2 && CurrentState == States.Idle && phase == 2){
			playerPosition = (GameObject.Find ("Player").transform.position);
			//movement decision 
			if (playerPosition.x > transform.position.x) {
				Xvalue =1;
			}
			else if (playerPosition.x < transform.position.x) {
				Xvalue = -1;
			}

			if (playerPosition.z >= 4.5f) {
				Yvalue =1;
			}
			else if (playerPosition.z < 4.5f) {
				Yvalue = -1;
			}
			else {
				Yvalue =0;
			}
			MoveTarget = new Vector3(playerPosition.x, playerPosition.y + (0.5f * Yvalue), playerPosition.z + (2 * Yvalue));
			changeState(States.PS2Movement);
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
		if (CurrentState == States.PS2Movement)
		{
			Ps2Movement();
		}
		if (CurrentState == States.PS2Attack)
		{
			Ps2Attack();
		}
	}
	


	void Idle(){
		attacked = 0;
		moved = 0;
	}

	//update the phase
	void NextPhase(){
		GetComponent<BossEnemyController>().health = 10;
		phase++;
		rigidbody.useGravity = true;
		changeState (States.Idle);
		//put call to spawning new minions in here
		if (phase == 2) {
			ESZone.wave = 1;
			ESZone.numWaves = 6;
			ESZone.phase = 2;
			ESZone.numEnemies = 2;
		}

		Reset = false;
	}

	//phase 1 traits
	void Ps1Movement(){
		transform.position = Vector3.MoveTowards (transform.position, MoveTarget, speed);

		if (MoveTarget == transform.position || distance <=8 && moved <1) {
			if (playerPosition.x > transform.position.x){
				MoveTarget.x = (playerPosition.x +9);
				if (playerPosition.z >=4){
					MoveTarget.z = (playerPosition.z +3);
				}
				else{
					MoveTarget.z = (playerPosition.z -3);
				}
			}
			if (playerPosition.x < transform.position.x){
				MoveTarget.x = (playerPosition.x -9);
				if (playerPosition.z >=4){
					MoveTarget.z = (playerPosition.z +3);
				}
				else{
					MoveTarget.z = (playerPosition.z -3);
				}
			}
			moved++;
		}

		if (distanceTarget <= 3 && moved >=1) {
			changeState(States.PS1Attack);
			moved = 0;
			MoveTarget = new Vector3(transform.position.x,transform.position.y, playerPosition.z);
		}
	}

	void Ps1Attack(){
		if (distanceTarget >2){
			transform.position = Vector3.MoveTowards (transform.position, MoveTarget, speed);
		}
		if(Time.time > nextBlast && (Vector3.Distance (MoveTarget, transform.position))<=2){
			anim.SetTrigger (AttackHash);
			// create bullet
			nextBlast = Time.time + delay;
			Instantiate(Bullet, transform.position, transform.rotation);
			attacked++;
		}
		if (attacked > 2) {
			changeState(States.Idle);
		}
		//voice bunny
		//gdc audio
	}

	//phase 2 traits
	void Ps2Movement(){
		transform.position = Vector3.MoveTowards (transform.position, MoveTarget, speed);

		if( distanceTarget <=2 && moved <1){
			MoveTarget2 = new Vector3(playerPosition.x + (15 * Xvalue), playerPosition.y, playerPosition.z);
			MoveTarget = MoveTarget2;
			moved++;
		}


		
		if (moved >=1 && distanceTarget <=1) {
			changeState(States.PS2Attack);
			MoveTarget = new Vector3(transform.position.x,playerPosition.y, playerPosition.z);
		}
	}
	
	void Ps2Attack(){
		if (distanceTarget >2){
			transform.position = Vector3.MoveTowards (transform.position, MoveTarget, speed);
		}
		else if(Time.time > nextBlast){
			if (BossCon.isRotated == true){

				bullet1 = new Vector3(transform.position.x +2, transform.position.y, transform.position.z);
				bullet2 = new Vector3(transform.position.x, transform.position.y+1, transform.position.z+1);
				bullet3 = new Vector3(transform.position.x, transform.position.y-1, transform.position.z-1);
			}
			if (BossCon.isRotated == false){
				
				bullet1 = new Vector3(transform.position.x -2, transform.position.y, transform.position.z);
				bullet2 = new Vector3(transform.position.x, transform.position.y+1, transform.position.z+1);
				bullet3 = new Vector3(transform.position.x, transform.position.y-1, transform.position.z-1);
			}
			nextBlast = Time.time + delay;
			attacked++;
			anim.SetTrigger (AttackHash);
			Instantiate(Bullet, bullet1, transform.rotation);
			Instantiate(Bullet, bullet2, transform.rotation);
			Instantiate(Bullet, bullet3, transform.rotation);
		}

		if (attacked > 1) {
			changeState(States.Idle);
		}

	}

	//phase 3 traits
	void Ps3Movement(){
		
	}
	
	void Ps3Attack(){
		
	}
	// move back to pedastal  
	void ResetMove(){
		Pedastal = (GameObject.FindWithTag ("Pedastal").transform.position);
		MoveTarget = (GameObject.FindWithTag ("Pedastal").transform.position);
		//rigidbody.AddForce((Pedastal - transform.position) * speed);
		transform.position = Vector3.MoveTowards (transform.position, Pedastal, speed);
		if (distanceTarget <= 0.5f){
			//put call to spawning new minions in after phase here
			if (phase == 1 && MinionStart == false) {
				MinionStart = true;
				NoEnemies = false;
				ESZone.wave = 1;
				ESZone.numWaves = 2;
				ESZone.phase = 1;
				ESZone.numEnemies = 4;
			}
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
