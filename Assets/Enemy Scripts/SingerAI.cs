using UnityEngine;
using System.Collections;

public class SingerAI : MonoBehaviour
{	
	public float distance;
	public Vector3 playerPosition;
	public float speed;
	public Sprite SingerSprite;

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
		//setting sprite
		//GetComponent<SpriteRenderer>().sprite = SingerSprite;
			//grabbing outside scripts and variables
		attackT = GetComponent<Attack>();
		moveBackE = GetComponent<moveBack> ();
		moveCloseE = GetComponent<Advance> ();

		}
	
		// Update is called once per frame
		void Update ()
		{ // getting player position and distance between
		playerPosition = (GameObject.Find ("Player").transform.position);
		distance = Vector3.Distance(playerPosition, transform.position);

			if (distance < 4) {
				changeState (States.Retreat);

			} else if (distance > 7) {
					changeState (States.Advance);
			
			} else if (distance>=5 && distance<=6){
				changeState (States.Attack);
			}
		}


		void Retreat(){
		moveBackE.enabled = true;
		moveCloseE.enabled = false;
		attackT.enabled = false;
		}
		void Advance(){
		moveCloseE.enabled = true;
		moveBackE.enabled = false;
		attackT.enabled = false;
		}
		void Init(){

		}

		void Attack(){
		attackT.enabled = true;
		moveCloseE.enabled = false;
		moveBackE.enabled = false;
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
			break;
		}
		case States.Retreat:
			{
			Retreat();
			break;
			}
		case States.Advance:
		{
			Advance();
			break;
		}
		case States.Attack:
		{
			Attack();
			break;
		}
		default:
		{ return;
		}
		}

	}

}