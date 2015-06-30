using UnityEngine;
using System.Collections;

public class BossEnemyController : MonoBehaviour
{	//set up variables and get controller
	public float health;
	public BossWolfAI BossWolf;

	public Vector3 playerPosition;
	public bool isRotated;
	public float dropItem;
	public GameObject HPdrop;
	public GameObject spark;
	public float shockTime;
	public float Shock;
	public float nextReactHit;
	Animator anim;
	int HitHash = Animator.StringToHash("Hit");
//	Vector3 movement = Vector3.zero;
		// Use this for initialization
		void Start ()
		{
		anim = GetComponent<Animator> ();
		//access to controller and other connected scripts
		BossWolf = GetComponent<BossWolfAI>();
		isRotated = false;
		// starts health check in 3 seconds and to repeat every second after
		//InvokeRepeating ("CheckHealth", 3, 1);
		//controller = GetComponent<CharacterController> ();
		}
	
		// Update is called once per frame
		void Update (){
			//Flipping to face player
			playerPosition = (GameObject.Find ("Player").transform.position);
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
			Destroy(gameObject, 0.5f);

		}
	} 
	

	public void GetHit(float dmg){
		health -= dmg;
		Instantiate(spark, transform.position, transform.rotation);

		if (health > 0 && nextReactHit <=Time.time) {
			anim.SetTrigger (HitHash);
			nextReactHit = Time.time + 1.5f;
		}
	}

	void OnTriggerEnter(Collider c){
		Vector3 PlayerPos = c.transform.position;
		if (c.gameObject.tag == "Player") {
			if(PlayerPos.x < transform.position.x){
			c.attachedRigidbody.AddForce(new Vector3(1 * -1, 1, 0) * 100);
			}
			else if(PlayerPos.x > transform.position.x){
				c.attachedRigidbody.AddForce(new Vector3(1 * 1, 1, 0) * 100);
			}
			if(PlayerPos.y < transform.position.y){
				c.attachedRigidbody.AddForce(new Vector3(1 * -1, 1, 0) * 100);
			}
			else if(PlayerPos.y > transform.position.y){
				c.attachedRigidbody.AddForce(new Vector3(1 * 1, 1, 0) * 100);
			}
		}

	}

}

