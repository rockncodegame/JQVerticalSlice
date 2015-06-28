using UnityEngine;
using System.Collections;

public class Disable_Building_Trigger2 : MonoBehaviour 
{
	//Variavle for first Alley and Wall
	public GameObject Alley;
	public GameObject Wall;
	
	void Start()
	{
		//Keeps the buildings visable and walls invisable when the trigger isnt active
		Alley.SetActive (true);
		Wall.SetActive (false);
	}
	void OnTriggerEnter(Collider other) 
	{
		//Keeps the buildings visable and walls invisable when the trigger is active
		Alley.SetActive (false);
		Wall.SetActive (true);
	}
	void OnTriggerExit(Collider other)
	{
		//Returns the buildings visablity and walls invisablity when the trigger isnt active
		Alley.SetActive (true);
		Wall.SetActive (false);
	}
}
