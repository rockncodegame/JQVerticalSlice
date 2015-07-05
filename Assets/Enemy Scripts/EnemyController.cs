using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{	//set up variables and get controller
	public float health;
	public SingerAI Singer;
	public DrummerAI Drummer;
	public GuitarAI Guitar;
	public Vector3 playerPosition;
	public bool isRotated;
	public float dropItem;
	public GameObject HPdrop;
	public GameObject spark;
	public GameObject p;
	public PlayerAttack pAttack;
	public float shockTime;
	public float Shock;
	Animator anim;
	int HitHash = Animator.StringToHash("Hit");
	int DeathHash = Animator.StringToHash("Death");
	//	Vector3 movement = Vector3.zero;
	// Use this for initialization
	void Start ()
	{
		//access to controller and other connected scripts
		Singer = GetComponent<SingerAI> ();
		Drummer = GetComponent<DrummerAI> ();
		Guitar = GetComponent<GuitarAI> ();
		p = GameObject.Find ("Player");
		isRotated = false;
		// starts health check in 2 seconds and to repeat every second after
		InvokeRepeating ("CheckHealth", 3, 1);
		//animation setups
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{ 
		
		//checking if still shocked
		if (shockTime <= Time.time) {
			Shock = 0;
			anim.SetFloat("Shock", Shock);
		}

		//Flipping to face player
		playerPosition = p.transform.position;
		if (playerPosition.x > transform.position.x && isRotated == false) {
			transform.Rotate(0, 180, 0); 
			isRotated = true;
		}
		if (playerPosition.x < transform.position.x && isRotated == true) {
			transform.Rotate(0, 180, 0); 
			isRotated = false;
		}
		
	}
	void CheckHealth(){	
		// kill if health hits zero
		if (health < 1) {
			//Dest.enabled = true;
			anim.SetTrigger (DeathHash);
			Destroy(gameObject,0.5f);
			dropItem = Random.Range(1,9);
			if (dropItem >5){
				//drop health
				Instantiate(HPdrop, transform.position, transform.rotation);
			}
		}
	} 
	
	public void GetHit(float dmg){
		
		health -= dmg;
		Instantiate(spark, transform.position, transform.rotation);
		if (health > 0) {
			anim.SetTrigger (HitHash);
			//checking if shocked
			anim.SetFloat("Shock", Shock);
		}
	}
	
}

