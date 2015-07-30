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
	public GameObject GuitarBullet;
	public GameObject DrummerBullet;
	public GameObject SingerBullet;
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

	public double delay = 2;
	public double attacked = 0;
	public float attack = 1;
	public enum States
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
	public States CurrentState = States.ResetMove;
	

	// Use this for initialization
	void Start () {
		Reset = false;
		GetComponent<BossEnemyController>().health = 10;
		phase=0;
		BossCon = GetComponent<BossEnemyController> ();
		rb = GetComponent<Rigidbody>();
		//looking at spawner
		p = GameObject.Find ("Player");
		BossES = GameObject.FindWithTag ("EnemyZone");
		ESZone = BossES.GetComponent<BossEnemyZone> ();
		NoEnemies = false;
		anim = GetComponent<Animator> ();
		
		speed = 8.5f * Time.deltaTime;
		playerPosition = (GameObject.Find ("Player").transform.position);
		MoveTarget = playerPosition;
		//pedastal 
		transform.position = Pedastal;
		Pedastal = (GameObject.FindWithTag ("Pedastal").transform.position);

	}
	
	// Update is called once per frame
	void Update () {
		// start next enemy set at zero health or end fight if last phase
		if (BossCon.health < 1 && phase != 3 && CurrentState != States.OverLook && CurrentState != States.Death && CurrentState != States.ResetMove) {
			changeState(States.ResetMove);

				}
		if (BossCon.health < 1 && phase == 3 && CurrentState != States.OverLook && CurrentState != States.Death && CurrentState != States.ResetMove) {
			changeState(States.Death);
			//put any death triggers here
		}
		p = GameObject.Find ("Player");

		if (p != null) {

			//DeathReset = true;

			Pedastal = (GameObject.FindWithTag ("Pedastal").transform.position);


			//distance calcs
			playerPosition = p.transform.position;
			distance = Vector3.Distance (playerPosition, transform.position);
			distanceTarget = Vector3.Distance (MoveTarget, transform.position);
		// if state is overlook doesnt need to check phase or movement
			if (CurrentState == States.Idle) {
						
						if (phase == 1 && Time.time >= delay) {
								delay = Time.time +4;
								attacked = 0;
								changeState (States.PS1Movement);
								anim.SetBool ("IsMoving", true);
								// starting phase 1 movement
						}
						if (phase == 2 && Time.time >= delay) {
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
								MoveTarget = new Vector3 (playerPosition.x, playerPosition.y, (playerPosition.z + (7 * Yvalue)));
								changeState (States.PS2Movement);
								anim.SetBool ("IsMoving", true);
								// starting phase 2 movement
						}
						if (phase == 3 && Time.time >= delay) {
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
								MoveTarget = new Vector3 (playerPosition.x, playerPosition.y, (playerPosition.z + (9 * Yvalue)));
								changeState (States.PS3Movement);
								anim.SetBool ("IsMoving", true);
								// starting phase 3 movement
						}
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
		anim.SetBool("IsMoving", false);
		minions = GameObject.FindGameObjectsWithTag ("Enemy");
			if (minions.Length == 0 && ESZone.wave >= ESZone.numWaves) {
				NoEnemies = true;
				NextPhase();
				delay = Time.time +2;
				changeState(States.Idle);
			}
	}



	void Idle(){
		moved = 0;
		anim.SetBool("IsMoving", false);
	}
	
	//update the phase
	void NextPhase(){
		transform.position = Vector3.MoveTowards (transform.position, playerPosition, speed);
		GetComponent<BossEnemyController>().health = 10;
		changeState (States.Idle);

		Reset = false;
		phase++;
	}
	
	//phase 1 traits
	void Ps1Movement(){
		transform.position = Vector3.MoveTowards (transform.position, playerPosition, speed);
		distance = Vector3.Distance (playerPosition, transform.position);
			if (distance <= 9){
				if (playerPosition.x >= 25){
				MoveTarget.x = playerPosition.x -8;
				}
				else{
				MoveTarget.x = playerPosition.x +8;
				}
				MoveTarget.y = playerPosition.y;
				MoveTarget.z = playerPosition.z;
				changeState(States.PS1Attack);

		}
	}
	
	void Ps1Attack(){
		if (distanceTarget >1){
			transform.position = Vector3.MoveTowards (transform.position, MoveTarget, speed);
		}
		else{
			anim.SetBool("IsMoving", false);
		}
		if(Time.time > nextBlast && (Vector3.Distance (MoveTarget, transform.position))<=2){
			anim.SetTrigger (AttackHash);
			// create bullet
			nextBlast = Time.time + 0.7f;
			Instantiate(GuitarBullet, transform.position, transform.rotation);
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
					
					bullet2 = new Vector3(transform.position.x, transform.position.y+1, transform.position.z+1);
					bullet3 = new Vector3(transform.position.x, transform.position.y-1, transform.position.z-1);
				}
				if (BossCon.isRotated == false){
					
					bullet2 = new Vector3(transform.position.x, transform.position.y+1, transform.position.z+1);
					bullet3 = new Vector3(transform.position.x, transform.position.y-1, transform.position.z-1);
				}
				nextBlast = Time.time + 1;
				attacked++;
				anim.SetTrigger (AttackHash);
				Instantiate(DrummerBullet, bullet2, transform.rotation);
				Instantiate(DrummerBullet, bullet3, transform.rotation);
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
				nextBlast = Time.time + 1.5f;
				attacked++;
				anim.SetTrigger (AttackHash);
				Instantiate(GuitarBullet, bullet2, transform.rotation);
				Instantiate(GuitarBullet, bullet3, transform.rotation);
				attack = 1;
			}
		}
		
		if (attacked >= 1) {
			changeState(States.Idle);
		}
		
	}
	
	//phase 3 traits
	void Ps3Movement(){
		distanceTarget = Vector3.Distance (MoveTarget, transform.position);
		transform.position = Vector3.MoveTowards (transform.position, MoveTarget, speed);
		if (moved ==1 && distanceTarget <=2) {
			changeState(States.PS3Attack);
			//MoveTarget = new Vector3(transform.position.x,playerPosition.y, playerPosition.z);
			anim.SetBool("IsMoving", false);
		}



		if (moved < 1) {
			MoveTarget2 = new Vector3(playerPosition.x + (9 * Xvalue), playerPosition.y, playerPosition.z);
		}

		if (distanceTarget <=2){
			MoveTarget = MoveTarget2;
			distanceTarget = Vector3.Distance (MoveTarget, transform.position);
			moved = 1;
		}

		

		
	}
	
	void Ps3Attack(){

		if (Time.time >= nextBlast && attack ==1){
			if (BossCon.isRotated == true){
				
				bullet2 = new Vector3(transform.position.x+1, transform.position.y+0.5f, transform.position.z+0.5f);
				bullet3 = new Vector3(transform.position.x+1, transform.position.y-0.5f, transform.position.z-0.5f);
			}
			if (BossCon.isRotated == false){
				
				bullet2 = new Vector3(transform.position.x-1, transform.position.y+0.5f, transform.position.z+0.5f);
				bullet3 = new Vector3(transform.position.x-1, transform.position.y-0.5f, transform.position.z-0.5f);
			}
			nextBlast = Time.time + 1.5f;
			attacked++;
			anim.SetTrigger (AttackHash);
			Instantiate(SingerBullet, bullet1, transform.rotation);
			Instantiate(SingerBullet, bullet2, transform.rotation);
			Instantiate(SingerBullet, bullet3, transform.rotation);
			attack = 2;
		
		}
		if (Time.time >= nextBlast && attack == 2){
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
			Instantiate(DrummerBullet, bullet2, transform.rotation);
			Instantiate(DrummerBullet, bullet3, transform.rotation);
			attack = 1;
		}
		
		if (attacked >= 2) {
			delay = Time.time + 2f;
			changeState(States.Idle);

		}
	}
	
	// move back to pedastal  
	void ResetMove(){
		anim.SetBool ("IsMoving", false);
		
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
			delay = Time.time +2f;
		}
	}
	

	
	void Death(){
		anim.SetTrigger (DeathHash);
		BossES.SetActive (false);
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
