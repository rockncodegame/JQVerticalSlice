using UnityEngine;
using System.Collections;

public class Level_Transition : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		Application.LoadLevel("mainmenu");
	}

}
