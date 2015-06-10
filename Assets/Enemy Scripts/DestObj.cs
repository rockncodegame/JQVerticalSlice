using UnityEngine;
using System.Collections;

public class DestObj : MonoBehaviour
{	
	public 
		// Use this for initialization
		void Start ()
		{
		//InvokeRepeating ("Destoyer", 3, 1);
		}
	
		// Update is called once per frame
		void Update ()
		{
			//destroy object script attached to
				Destroy(gameObject,2);
			
		}
	
}


