using UnityEngine;
using System.Collections;

public class BossWolfAI : MonoBehaviour {
	public float phase;
	public bool Reset;
	public bool NoEnemies;
	public bool MinionStart;
	public GameObject[] minions;
	public GameObject p;
	public BossEnemyController BossCon;
	public bool DeathReset;
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
	public float moved;
	public float beat;
	//bullet variables
	public Rigidbody projectile;
	public GameObject Bullet;
	//for moving around player
	public float Xvalue;
	public float Yvalue;
	public Vector3 MoveTarget2;
	public GameObject PedastalGround;
	public Vector3 bullet1;
	public Vector3 bullet2;
	public Vector3 bullet3;
	public Vector3 bullet4;
	public float e;
	//animation
	Animator anim;
	public Rigidbody rb;
	int AttackHash = Animator.StringToHash("Attack");
	int RoarHash = Animator.StringToHash("Roar");
	int DeathHash = Animator.StringToHash("Death");
	
	double nextBlast=0;
	double nextMove;
	double delay = 2;
	public double attacked = 0;
	public float attack = 1;
	enum States
	{
		OverLook,
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
	States CurrentState = States.ResetMove;
	
	
	// Use this for initialization
	void Start () {
		InvokeRepeating ("BeatTime", 2,1);
		Reset = false;
		GetComponent<BossEnemyController>().health = 10;
		phase=0;
		BossCon = GetComponent<BossEnemyController> ();
		rb = GetComponent<Rigidbody>();
		//looking at spawner
		p = GameObject.Find ("Player");
		BossES = GameObject.FindWithTag ("BossZone");
		ESZone = BossES.GetComponent<BossEnemyZone> ();
		NoEnemies = false;
		anim = GetComponent<Animator> ();
		
		speed = 7.5f * Time.deltaTime;
		playerPosition = (GameObject.Find ("Player").transform.position);
		MoveTarget = playerPosition;
		//pedastal 
		Pedastal = (GameObject.FindWithTag ("Pedastal").transform.position);

	}
	
	// Update is called once per frame
	void Update () {
		p = GameObject.Find ("Player");
		if (p == null) {
			//reset phase on player death
			if(CurrentState == States.OverLook){
				phase = phase;
			}
			else if (DeathReset == true && CurrentState != States.OverLook){
				phase = phase -1;
				DeathReset = false;
			}
			changeState (States.ResetMove);
		}
		if (p != null) {
			minions = GameObject.FindGameObjectsWithTag ("Enemy");
			if (minions.Length == 0 && ESZone.wave >= ESZone.numWaves) {
				NoEnemies = true;
			}
			DeathReset = true;

			Pedastal = (GameObject.FindWithTag ("Pedastal").transform.position);
			//can be changed as phases increase

			if (phase == 3 && BossHP <= 0 && NoEnemies == true) {
				anim.SetTrigger (DeathHash);
				Destroy (gameObject, 1);
			}
			
			//update health
			BossHP = BossCon.health;
			if (BossHP < 1 && phase < 3 && NoEnemies == true) {

				changeState (States.ResetMove);
			}
			
		
			//distance calcs
			playerPosition = p.transform.position;
			distance = Vector3.Distance (playerPosition, transform.position);
			distanceTarget = Vector3.Distance (MoveTarget, transform.position);
		
		


		
			if (beat == 2 && CurrentState == States.Idle && phase == 1) {
				changeState (States.PS1Movement);
				anim.SetBool ("IsMoving", true);
				attacked = 0;
				moved = 0;
				MoveTarget = playerPosition;
				MoveTarget.y = MoveTarget.y + 1;
			}
			if (beat == 2 && CurrentState == States.Idle && phase == 2) {
				playerPosition = p.transform.position;
				//movement decision 
				if (playerPosition.x > transform.position.x) {
					Xvalue = 1;
				} else if (playerPosition.x < transform.position.x) {
					Xvalue = -1;
				}
			
				if (playerPosition.z >= 1.5f) {
					Yvalue = 1;
				} else if (playerPosition.z < 1.5f) {
					Yvalue = -1;
				}
				attacked = 0;
				moved = 0;
				MoveTarget = new Vector3 (playerPosition.x, playerPosition.y, (playerPosition.z + (5 * Yvalue)));
				changeState (States.PS2Movement);
				anim.SetBool ("IsMoving", true);
			
			}
			//phase 3 sets
			if (beat == 2 && CurrentState == States.Idle && phase == 3) {
				playerPosition = p.transform.position;
				//movement decision 
				if (playerPosition.x >= 15) {
					Xvalue = -1;
				}
				if (playerPosition.x <= -15) {
					Xvalue = 1;
				}
				if (playerPosition.x >= 5 && playerPosition.x < 14) {
					Xvalue = 1;
				}
				if (playerPosition.x <= -5 && playerPosition.x > -14) {
					Xvalue = -1;
				}
				if (playerPosition.z >= 1.5f) {
					Yvalue = 1;
				} else if (playerPosition.z < 1.5f) {
					Yvalue = -1;
				}
				attacked = 0;
				moved = 0;
				MoveTarget = new Vector3 (playerPosition.x, playerPosition.y, (playerPosition.z + (4 * Yvalue)));
				changeState (States.PS3Movement);
				anim.SetBool ("IsMoving", true);
			}
		}
		// matching behavior to state
		if (CurrentState == States.ResetMove)
		{
			ResetMove();
		}
		if (CurrentState == States.OverLook)
		{
			OverLook();
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
		if (CurrentState == States.PS3Movement)
		{
			Ps3Movement();
		}
		if (CurrentState == States.PS3Attack)
		{
			Ps3Attack();
		}
	}
	
	//While enemies are spawned
	void OverLook(){
		minions = GameObject.FindGameObjectsWithTag ("Enemy");
		anim.SetBool("IsMoving", false);
		attacked = 0;
		moved = 0;

		//change phase on health hit 0
		if (Time.time >= delay){
			if (CurrentState==States.OverLook && NoEnemies == true) {
				NextPhase();
				//reset time
				beat=0;
			}	
		}
	}
	//reset phase on death
	void DeathCheck(){

		if (p == null) {
			p = GameObject.Find ("Player");
			//reset phase on player death
			phase = phase - 1;
			changeState (States.ResetMove);
		}
	}


	void Idle(){
		attacked = 0;
		moved = 0;
		anim.SetBool("IsMoving", false);
	}
	
	//update the phase
	void NextPhase(){
		transform.position = Vector3.MoveTowards (transform.position, playerPosition, speed);
		GetComponent<BossEnemyController>().health = 10;
		changeState (States.Idle);

		/*deleting all old enemies
		minions = GameObject.FindGameObjectsWithTag ("Enemy");
		for(var i = 0 ; i < minions.Length ; i ++)
		{
			Destroy(minions[i]);
		}
		*/
		//put call to spawning new minions in here
		Reset = false;
		phase++;
	}
	
	//phase 1 traits
	void Ps1Movement(){
		transform.position = Vector3.MoveTowards (transform.position, MoveTarget, speed);
		
		if (MoveTarget == transform.position || distance <=8 && moved <1) {
			if (playerPosition.x > transform.position.x){
				MoveTarget.x = (playerPosition.x +10);
				if (playerPosition.z >=4){
					MoveTarget.z = (playerPosition.z +5);
				}
				else{
					MoveTarget.z = (playerPosition.z -5);
				}
			}
			if (playerPosition.x < transform.position.x){
				MoveTarget.x = (playerPosition.x -10);
				if (playerPosition.z >=4){
					MoveTarget.z = (playerPosition.z +5);
				}
				else{
					MoveTarget.z = (playerPosition.z -5);
				}
			}
			moved++;
		}
		
		if (distanceTarget <= 5 && moved >=1) {
			changeState(States.PS1Attack);
			moved = 0;
			MoveTarget = new Vector3(transform.position.x,transform.position.y, playerPosition.z);
			anim.SetBool("IsMoving", false);
		}
	}
	
	void Ps1Attack(){
		if (distanceTarget >2){
			transform.position = Vector3.MoveTowards (transform.position, MoveTarget, speed);
		}
		else{
			anim.SetBool("IsMoving", false);
		}
		if(Time.time > nextBlast && (Vector3.Distance (MoveTarget, transform.position))<=2){
			anim.SetTrigger (AttackHash);
			// create bullet
			nextBlast = Time.time + 0.5f;
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
		
		if (moved >=1 && distanceTarget <=2) {
			changeState(States.PS2Attack);
			MoveTarget = new Vector3(transform.position.x,playerPosition.y, playerPosition.z);
			anim.SetBool("IsMoving", false);
		}
		if(moved <1){
			MoveTarget2 = new Vector3(playerPosition.x + (10 * Xvalue), playerPosition.y, playerPosition.z);
		}
		if (distanceTarget <=2){
			moved = 1;
			MoveTarget = MoveTarget2;
		}
		
		
		
		
		
	}
	
	void Ps2Attack(){
		
		if(Time.time > nextBlast){
			if (attack == 1){
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
				nextBlast = Time.time + 1;
				attacked++;
				anim.SetTrigger (AttackHash);
				Instantiate(Bullet, bullet1, transform.rotation);
				Instantiate(Bullet, bullet2, transform.rotation);
				Instantiate(Bullet, bullet3, transform.rotation);
				attack = 2;
			}
			if (attack == 2){
				if (BossCon.isRotated == true){
					
					bullet2 = new Vector3(transform.position.x+1, transform.position.y+0.5f, transform.position.z+0.5f);
					bullet3 = new Vector3(transform.position.x+1, transform.position.y-0.5f, transform.position.z-0.5f);
				}
				if (BossCon.isRotated == false){
					
					bullet2 = new Vector3(transform.position.x-1, transform.position.y+0.5f, transform.position.z+0.5f);
					bullet3 = new Vector3(transform.position.x-1, transform.position.y-0.5f, transform.position.z-0.5f);
				}
				nextBlast = Time.time + 1;
				attacked++;
				anim.SetTrigger (AttackHash);
				Instantiate(Bullet, bullet1, transform.rotation);
				Instantiate(Bullet, bullet2, transform.rotation);
				Instantiate(Bullet, bullet3, transform.rotation);
				attack = 1;
			}
		}
		
		if (attacked >= 1) {
			changeState(States.Idle);
		}
		
	}
	
	//phase 3 traits
	void Ps3Movement(){
		transform.position = Vector3.MoveTowards (transform.position, MoveTarget, speed);
		
		if (distanceTarget <=2){
			MoveTarget = MoveTarget2;
			moved = 1;
		}
		if (moved < 1) {
			MoveTarget2 = new Vector3(playerPosition.x + (8 * Xvalue), playerPosition.y, playerPosition.z);
		}
		
		if (moved ==1 && distanceTarget <=3) {
			changeState(States.PS3Attack);
			MoveTarget = new Vector3(transform.position.x,playerPosition.y, playerPosition.z);
			anim.SetBool("IsMoving", false);
		}
		
	}
	
	void Ps3Attack(){
		if (Time.time >= nextBlast){
			if (BossCon.isRotated == true){
				
				bullet2 = new Vector3(transform.position.x+1, transform.position.y+0.5f, transform.position.z+0.5f);
				bullet3 = new Vector3(transform.position.x+1, transform.position.y-0.5f, transform.position.z-0.5f);
			}
			if (BossCon.isRotated == false){
				
				bullet2 = new Vector3(transform.position.x-1, transform.position.y+0.5f, transform.position.z+0.5f);
				bullet3 = new Vector3(transform.position.x-1, transform.position.y-0.5f, transform.position.z-0.5f);
			}
			nextBlast = Time.time + 1f;
			attacked++;
			anim.SetTrigger (AttackHash);
			Instantiate(Bullet, bullet1, transform.rotation);
			Instantiate(Bullet, bullet2, transform.rotation);
			Instantiate(Bullet, bullet3, transform.rotation);
			attack = 2;
		}
		if (Time.time >= nextBlast && attack == 1){
			if (BossCon.isRotated == true){
				
				bullet2 = new Vector3(transform.position.x+1, transform.position.y-1f, transform.position.z+2f);
				bullet3 = new Vector3(transform.position.x+1, transform.position.y+1f, transform.position.z-2f);
			}
			if (BossCon.isRotated == false){
				
				bullet2 = new Vector3(transform.position.x-1, transform.position.y-1f, transform.position.z+2f);
				bullet3 = new Vector3(transform.position.x-1, transform.position.y+1f, transform.position.z-2f);
			}
			nextBlast = Time.time + 1.5f;
			attacked++;
			anim.SetTrigger (AttackHash);
			Instantiate(Bullet, bullet1, transform.rotation);
			Instantiate(Bullet, bullet2, transform.rotation);
			Instantiate(Bullet, bullet3, transform.rotation);
			attack = 1;
		}
		
		if (attacked >= 2) {
			changeState(States.Idle);
		}
	}
	
	// move back to pedastal  
	void ResetMove(){
		
		
		Pedastal = (GameObject.FindWithTag ("Pedastal").transform.position);
		MoveTarget = (GameObject.FindWithTag ("Pedastal").transform.position);
		//rigidbody.AddForce((Pedastal - transform.position) * speed);
		transform.position = Pedastal;
		if (Time.time >= delay){
			GetComponent<BossEnemyController>().health = 10;
			if (phase == 0) {
				NoEnemies = false;
				ESZone.wave =1;
				ESZone.numWaves = 2;
				ESZone.phase = 1;
				ESZone.numEnemies = 2;
			}
			//put call to spawning new minions in after phase here
			//phase2
			if (phase == 1) {
				NoEnemies = false;
				ESZone.phase = 2;
				ESZone.wave = 1;
				ESZone.numWaves = 2;
				ESZone.numEnemies = 2;
			}
			//phase 3
			if (phase == 2) {
				NoEnemies = false;
				ESZone.phase = 3;
				ESZone.wave = 1;
				ESZone.numWaves = 3;
				ESZone.numEnemies = 3;
			}
			changeState(States.OverLook);
			NoEnemies = false;
			anim.SetTrigger (RoarHash);
			delay = Time.time +1.5f;
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
		case States.OverLook:
		{
			OverLook();
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
