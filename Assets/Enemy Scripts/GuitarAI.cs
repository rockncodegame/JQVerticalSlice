using UnityEngine;
using System.Collections;

public class GuitarAI : MonoBehaviour
{
	
	public float distance;
	public Vector3 playerPosition;
	public float speed;
	public float beat;
	public float strafe;
	public Vector3 newPosistion;
	public float nextMove = 6;
	public GameObject Bullet;
	public GameObject p;
	
	//attack variables
	double nextBlast=0;
	double delay = 3;
	double attacked = 0;
	
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
	
	States CurrentState = States.Idle;
	// Use this for initialization
	void Start (){
		//grabbing outside scripts and variables
		GetComponent<EnemyController>().health = 4;
		anim = GetComponent<Animator> ();
		InvokeRepeating ("BeatTime", 2,1);
		speed = 4 * Time.deltaTime;
		p = GameObject.Find ("Player");
		newPosistion = p.transform.position;
		//setting sprite
		//GetComponent<SpriteRenderer>().sprite = BassSprite;
		//initial position
	}
	
	// Update is called once per frame
	void Update (){ 
		if (p != null) {
			playerPosition = p.transform.position;
			distance = Vector3.Distance(playerPosition, transform.position);

			if(beat == 4 || beat == 12){
				if (distance >=5){
				newPosistion = playerPosition;
				}
				if (strafe == 1){

					newPosistion.z = playerPosition.z +(3*strafe);
					strafe = strafe * -1;
				}
				else if (strafe == -1){
					newPosistion.z = playerPosition.z +(3*strafe);
					strafe = strafe * -1;
				}
				while(newPosistion.x >= (playerPosition.x -1)  && newPosistion.x <= (playerPosition.x +1)){
					newPosistion = (Random.onUnitSphere * 2 + playerPosition);
				}
				if (newPosistion.z <2){
					newPosistion.z = (playerPosition.z +2);
				}
				newPosistion.y = (playerPosition.y +2);
			}
		}
		
		if(Time.time >= nextMove){
			changeState(States.Advance);
			attacked = 0;
			nextMove = Time.time + 7;
		}
		
		if (CurrentState == States.Advance) {
			Advance ();
		}
		if (CurrentState == States.Idle) {
			Idle ();
		}
		if (CurrentState == States.Attack) {
			Attack ();
		}
		
	}
	
	
	void Retreat(){
		
	}
	void Advance(){
		transform.position = Vector3.MoveTowards (transform.position, newPosistion, speed);

		if (distance <= 8){ 
			newPosistion.z = (playerPosition.z + Random.Range(-2,2));
			newPosistion.y = transform.position.y;
			newPosistion.x = transform.position.x;
			CurrentState = States.Attack;
		}
	}
	void Idle(){
	}
	
	void Attack(){
		transform.position = Vector3.MoveTowards (transform.position, newPosistion, speed);
		if(transform.position.x < (newPosistion.x +3) && transform.position.x > (newPosistion.x -3)){
			if(Time.time > nextBlast){
				//animate
				anim.SetTrigger (AttackHash);
				// create bullet
				nextBlast = Time.time + delay;
				Instantiate(Bullet, transform.position, transform.rotation);
				Bullet.rigidbody.AddForce(Bullet.transform.forward * 2);
				attacked++;
			}
			if (attacked > 1) {
				changeState(States.Idle);
			}
		}
	}
	
	void BeatTime(){
		//self kept beat
		if(beat == 16){
			beat = 0;
		}
		beat++;
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