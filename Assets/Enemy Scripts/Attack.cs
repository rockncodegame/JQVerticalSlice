using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
	public Rigidbody projectile;
	public GameObject Bullet;
	public SingerAI Singer;
	public DrummerAI Drummer;
	public GuitarAI Guitar;
	double nextBlast=0;
	double delay = 2;
		// Use this for initialization
		void Start ()
		{
		// set delay for each enemy

		//set bullet fire to every 5 seconds after first
		//nextBlast = Time.time + delay;

		}
		// replay once per time
		void Update ()
		{
		if(Time.time > nextBlast){
		// create bullet
			nextBlast = Time.time + delay;
			Instantiate(Bullet, transform.position, transform.rotation);
			Bullet.rigidbody.AddForce(Bullet.transform.forward * 2);
			}
		}

}

