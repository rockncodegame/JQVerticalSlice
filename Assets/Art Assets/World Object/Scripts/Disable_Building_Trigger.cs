using UnityEngine;
using System.Collections;

public class Disable_Building_Trigger : MonoBehaviour 
{
	//Variavle for first Alley and Wall
	public GameObject Alley_1;
	public GameObject Wall_1;
	//Variavle for second Alley and Wall
	public GameObject Alley_2;
	public GameObject Wall_2;
	//Variavle for third Alley and Wall
	public GameObject Alley_3;
	public GameObject Wall_3;
	//Variavle for fourth Alley and Wall
	public GameObject Alley_4;
	public GameObject Wall_4;
	//Keeps track of which Trigger was pressed
	int Alley_Count = 0;


	void Start()
	{
		//Keeps the buildings visable and walls invisable when the trigger isnt active
		Alley_1.SetActive (true);
		Wall_1.SetActive (false);

		Alley_2.SetActive (true);
		Wall_2.SetActive (false);

		Alley_3.SetActive (true);
		Wall_3.SetActive (false);

		Alley_4.SetActive (true);
		Wall_4.SetActive (false);
	}
	void OnTriggerEnter(Collider other) 
	{
		//Keeps the buildings visable and walls invisable when the trigger is active
		if (Alley_Count == 1) 
		{
			Alley_1.SetActive (false);
			Wall_1.SetActive (true);
		}

		else if(Alley_Count == 2) {
			Alley_2.SetActive (false);
			Wall_2.SetActive (true);
		}

		else if(Alley_Count == 3){ 
			Alley_3.SetActive (false);
			Wall_3.SetActive (true);
		}

		else if(Alley_Count == 4)
		{ 
		Alley_4.SetActive (false);
		Wall_4.SetActive (true);
		}
	}
	void OnTriggerExit(Collider other)
	{
		//Returns the buildings visablity and walls invisablity when the trigger isnt active
		Alley_1.SetActive (true);
		Wall_1.SetActive (false);
		
		Alley_2.SetActive (true);
		Wall_2.SetActive (false);
		
		Alley_3.SetActive (true);
		Wall_3.SetActive (false);
		
		Alley_4.SetActive (true);
		Wall_4.SetActive (false);
	}
}
