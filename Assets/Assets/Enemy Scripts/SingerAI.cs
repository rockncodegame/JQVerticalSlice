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

	enum States
	{
		Init,
		Retreat,
		Attack,
		Advance,
		Death,
	};

	private States CurrentState = States.Init;

	public GameObject Player; 
	public moveBack moveBackE;
	public Attack attackT;
	public Advance moveCloseE;

		// Use this for initialization
		void Start ()
		{
		GetComponent<EnemyController>().health = 3;
		strafeMod = Random.Range(1,2);
		//setting sprite
		//GetComponent<SpriteRenderer>().sprite = SingerSprite;
			//grabbing outside scripts and variables
		attackT = GetComponent<Attack>();
		moveBackE = GetComponent<moveBack> ();
		moveCloseE = GetComponent<Advance> ();
		InvokeRepeating ("BeatTime", 2,1);
		speed = 3 * Time.deltaTime;
		}
	
		// Update is called once per frame
		void Update ()
		{ // getting player position and distance between
		playerPosition = (GameObject.Find ("Player").transform.position);
		distance = Vector3.Distance(playerPosition, transform.position);

		if (distance < 4 && beat == 3 || beat == 9) {
			changeState(States.Retreat);

			} else if (distance >= 6) {
			changeState(States.Advance);
			} else {
			changeState(States.Attack);
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
		//moveBackE.enabled = true;
		attackT.enabled = false;
		moveCloseE.enabled = false;
		speed = -5 * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, playerPosition, speed);
		if (distance >= 4) {
			changeState (States.Attack);
		}
		}
		void Advance(){
		//moveCloseE.enabled = true;
		moveBackE.enabled = false;
		attackT.enabled = false;
		speed = 2 * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, playerPosition, speed);

		if (distance <= 5 ) {
			changeState (States.Attack);
		}
		}
		void Init(){

		}

		void Attack(){
		attackT.enabled = true;
		moveCloseE.enabled = false;
		moveBackE.enabled = false;

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
		case States.Init:
		{
			Init();
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