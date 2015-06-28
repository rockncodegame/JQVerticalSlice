using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour, IVisModifierTarget {
	//declaring variables
	//public GameObject bulletSource;
	public GameObject b, b2, b3, barrier;
	public float comboTime1;
	public static bool fire, wind, elec;
	public static int ult = 0;
	public int combo, pick, dir;
	public MoveTest pMove;
	public bool isAttacking, isBlocking;
	public GameObject[] attacks;
	//update rhythm meter
	public GameObject p;
	public PlayerStats stats;
	private int beatTrack;

	// Use this for initialization
	void Start () {
		comboTime1 = 0.0f;
		combo = 0;
		pMove = GetComponent<MoveTest> ();
		fire = wind = elec = isAttacking = false;
		barrier.SetActive (false);
		//rhythm meter declaration
		p = GameObject.FindGameObjectWithTag ("Player");
		stats = p.GetComponent<PlayerStats> ();
		beatTrack = 0;
	}
	
	// Update is called once per frame
	void Update () {
		barrier.transform.position = transform.position;

		if (pMove.isRight == 1)
			dir = 1;
		if (pMove.isRight == -1)
			dir = -1;
		//change pick deterines which attack you're using
		ChangePick ();

		//sets the position for where the attack will spawn
		Vector3 bPos = new Vector3 (transform.position.x + (1.5f * dir), transform.position.y, transform.position.z);

		//if the combo time runs out, revert to normal state
		//destroy any lingering attack sprites if they're there
		if (Time.time > comboTime1) {
			combo = 0;
			GetComponent<SpriteRenderer>().color = Color.blue;
			if (b != null){
				Destroy(b);
			}
			if (b2 != null){
				Destroy(b2);
			}
			if (b3 != null){
				Destroy(b3);
			}
			isAttacking = false;
		}

		if (Input.GetKeyDown (KeyCode.X)){
			barrier.SetActive(true);
			isBlocking = true;
		}

		if (Input.GetKeyUp (KeyCode.X)){
			barrier.SetActive(false);
			isBlocking = false;
		}

		//first attack
		if (Input.GetKeyDown (KeyCode.Z) && Time.time > comboTime1 && combo == 0 && !isBlocking) {
			isAttacking = true;
			combo = 1;
			comboTime1 = Time.time + 1.0f;
			b = Instantiate (attacks[pick], bPos, Quaternion.identity) as GameObject;
			b.transform.localScale = new Vector3 (b.transform.localScale.x * dir, b.transform.localScale.y, b.transform.localScale.z);
		}
		//if the player continues the combo before the time is used, do the next move
		//if the first attack sprite is still there, destroy it
		//change the player state
		//create the new attack and then change its color based on the key
		//scale the attack up to signify a bigger attack
		else if (combo == 1 && Time.time < comboTime1 && Input.GetKeyDown (KeyCode.Z) && !isBlocking){
			if (b != null)
				Destroy (b);
			combo = 2;
			GetComponent<SpriteRenderer>().color = Color.white;
			b2 = Instantiate (attacks[pick], bPos, Quaternion.identity) as GameObject;
			b2.transform.localScale = new Vector3 (b2.transform.localScale.x * dir + .05f*dir, b2.transform.localScale.y + .05f, b2.transform.localScale.z);
		}
		//if the player continues the combo before the time is used, do the next move
		//if the first attack sprite is still there, destroy it
		//change the player state
		//create the new attack and then change its color based on the key
		//scale the attack up to signify a bigger attack
		else if (combo == 2 && Time.time < comboTime1 && Input.GetKeyDown (KeyCode.Z) && !isBlocking){
			if (b2 != null)
				Destroy (b2);
			combo = 3;
			b3 = Instantiate (attacks[pick], bPos, Quaternion.identity) as GameObject;
			b3.transform.localScale = new Vector3 (b3.transform.localScale.x * dir + .10f * dir, b3.transform.localScale.y + .10f, b3.transform.localScale.z);
		}
	}

	//changes picks based on the the key pressed.
	//1 = fire (red)
	//2 = wind (green)
	//3 = electricity (light blue)
	//4 = ultimate (black)
	public void ChangePick () {
		if (Input.GetKeyDown (KeyCode.Alpha1) && fire)
			pick = 1;
		if (Input.GetKeyDown (KeyCode.Alpha2) && wind)
			pick = 2;
		if (Input.GetKeyDown (KeyCode.Alpha3) && elec)
			pick = 3;
		if (Input.GetKeyDown (KeyCode.Alpha4) && ult == 3)
			pick = 4;
	}

	//rhythm meter update function
	public void OnValueUpdated(float current, float previous, float difference, float adjustedDifference){
		if (adjustedDifference > 0.1 && combo > 0) {
			Debug.Log ("Attacked On Beat");
			stats.rhythm += 1;
		}
		if (adjustedDifference > 0.1) {
			beatTrack++;
			Debug.Log ("Beat " + beatTrack);
		}
	}
}
