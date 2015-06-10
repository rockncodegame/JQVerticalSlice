using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
	public Rigidbody projectile;
	public GameObject Bullet;

		// Use this for initialization
		void Start ()
		{
		//set bullet fire to every 5 seconds after first
		InvokeRepeating ("Blast", 0,5);
		}
		// replay once per time
		void Blast ()
		{
		// create bullet
		Instantiate(Bullet, transform.position, transform.rotation);
		Bullet.rigidbody.AddForce(Bullet.transform.forward * 2);
		}

}

