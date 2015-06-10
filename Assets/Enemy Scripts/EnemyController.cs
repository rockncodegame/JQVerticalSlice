using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{	//set up variables and get controller
	public float health;
	public SingerAI Singer;
	public DrummerAI Drummer;
	public GuitarAI Guitar;
	public DestObj Dest;
	Vector3 movement = Vector3.zero;
		// Use this for initialization
		void Start ()
		{
		//access to controller and other connected scripts
		Singer = GetComponent<SingerAI> ();
		Drummer = GetComponent<DrummerAI> ();
		Guitar = GetComponent<GuitarAI> ();
		Dest = GetComponent<DestObj> ();
		// starts health check in 2 seconds and to repeat every second after
		InvokeRepeating ("CheckHealth", 2, 1);
		// test damage
		//InvokeRepeating ("TestDamage", 5, 5);
		//controller = GetComponent<CharacterController> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
		//
		/*if (controller.isGrounded == false)
						movement.y += Physics.gravity.y * Time.deltaTime;
				
		controller.Move (movement * Time.deltaTime);
		*/
		}
	 void CheckHealth(){	
		// kill if health hits zero
		if (health < 1) {
			Dest.enabled = true;
		}
	} 
	void TestDamage(){
		health--;
		}
	public void GetHit(float dmg){
		health -= dmg;
	}
}

