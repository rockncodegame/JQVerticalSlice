using UnityEngine;
using System.Collections;

public class GuitarAI : MonoBehaviour
{
	
	public float distance;
	public Vector3 playerPosition;
	public float speed;
	public float beat;
	public Vector3 newPosistion;

	enum States
	{
		Idle,
		Retreat,
		Attack,
		Advance,
		Death,
	};
	
	States CurrentState = States.Idle;
	
	public GameObject aPlayer; 
	public moveBack moveBackE;
	public Attack attackT;
	public Advance moveCloseE;
	// Use this for initialization
	void Start ()
	{
		
		//grabbing outside scripts and variables
		GetComponent<EnemyController>().health = 5;
		attackT = GetComponent<Attack>();
		moveBackE = GetComponent<moveBack> ();
		moveCloseE = GetComponent<Advance> ();
		InvokeRepeating ("BeatTime", 2,1);
		speed = 3 * Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update ()
	{ 
		playerPosition = (GameObject.Find ("Player").transform.position);
		distance = Vector3.Distance(playerPosition, transform.position);

		if(beat == 2|| beat == 12){
			newPosistion = Random.onUnitSphere * 2 + playerPosition;
			while(newPosistion.x >= (playerPosition.x -1)  && newPosistion.x <= (playerPosition.x +1)){
				newPosistion = Random.onUnitSphere * 2 + playerPosition;
			}
			newPosistion.y = playerPosition.y;
		}

		if(beat == 6|| beat == 16){
			changeState(States.Advance);
	
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
		attackT.enabled = false;
		transform.position = Vector3.MoveTowards (transform.position,newPosistion , speed);

		if (transform.position.x >= (newPosistion.x -1) && transform.position.x <= (newPosistion.x +1)){ 
			CurrentState = States.Attack;
		}
	}
	void Idle(){
		attackT.enabled = false;
	}
	
	void Attack(){
		attackT.enabled = true;

		//changeState (States.Idle);

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