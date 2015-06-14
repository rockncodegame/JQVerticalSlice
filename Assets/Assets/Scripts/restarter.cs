using System;
using UnityEngine;

public class restarter : MonoBehaviour {

	


		
			private void OnTriggerEnter(Collider other)
			{
				if (other.tag == "Player")
				{
					Application.LoadLevel("test2");
				}
			}

	}
