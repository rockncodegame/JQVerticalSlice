
using UnityEngine;
using System.Collections;

public abstract class EnemyBehaviour : MonoBehaviour {
	public float distance;
	public float speed = 1 * Time.deltaTime;
	public float health;
	public Vector3 playerPosition;
	
	/*Here you can add and remove states for the enemy, see the manual.txt for guidance!*/
	public enum EnemyState
	{
		initializing,
		idle,
		sawPlayer,
		chasing,
		attacking,
		fleeing
	}
	/*This is the currentState of the Enemy, this is what you'll change in the child-Class*/
	public EnemyState currentState;

	
	void Start () {
		currentState = EnemyState.initializing;
	}
	
	/*In here there is a switch-statement which handles which method that is going
        * to be updating, this is chosen by the currentState of the enemy.
         It is in here that you will add your own EnemyState.yourState-case and call for your own method below*/
	public virtual void Update () {
		 playerPosition = (GameObject.Find ("Player").transform.position);
		distance = Vector3.Distance(playerPosition, transform.position);
		switch (currentState) {
		case EnemyState.initializing:
			/*filling in the player reference for easier access*/
			/*playerReference = GameObject.Find ("Player");*/
			currentState = EnemyState.idle;
			break;
		case EnemyState.idle:
			Idle();
			break;
		case EnemyState.sawPlayer:
			SawPlayer();
			break;
		case EnemyState.chasing:
			Chasing();

			break;
		case EnemyState.attacking:
			Attacking();

			currentState = EnemyState.idle;
			break;
		case EnemyState.fleeing:
			Fleeing();

			currentState = EnemyState.idle;
			break;
		default:
			break;
		}
	}
	
	/*When you add your own methods here they need to be virtual, this is so you can in override them in your own
         class*/
	
	public virtual void Idle()
	{
		if (distance <=20){
			currentState = EnemyState.fleeing;
		}
		else if (distance >=40){
			currentState = EnemyState.chasing;
		}
		else {
			currentState = EnemyState.attacking;
		}
	}
	public virtual void SawPlayer()
	{
	}
	public virtual void Chasing()
	{

//		int positionX = (Random.Range (6, 15));
//		int positionZ = (Random.Range (6, 15));
		transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed); 

	}
	public virtual void Attacking()
	{

		currentState = EnemyState.idle;
	}
	public virtual void Fleeing()
	{

		currentState = EnemyState.idle;
	}
}
