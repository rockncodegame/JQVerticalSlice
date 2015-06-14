using UnityEngine;
using System.Collections;

public class Strafe : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		InvokeRepeating ("ToLeft", 1, 2);
		InvokeRepeating ("ToRight", 5, 20);
	}
	
	// Update is called once per frame
	void ToLeft ()
	{
		transform.Translate(1, 1, Time.deltaTime);
	}
	 void ToRight()
	{
		transform.Translate(-5, 5, Time.deltaTime);
	}
}

