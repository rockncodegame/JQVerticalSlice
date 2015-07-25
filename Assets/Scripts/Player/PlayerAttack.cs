using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour, IVisModifierTarget {
	//declaring variables
	//public GameObject bulletSource;
	public GameObject b, b2, b3, barrier;
	public float comboTime1;
	public static bool fire, wind, elec, ult1, ult2, ult3;
	public int combo, pick, dir;
	public MoveTest pMove;
	public bool isAttacking, isBlocking;
	public GameObject[] attacks;
	//update rhythm meter
	public GameObject p;
	public PlayerStats stats;
	private int beatTrack;

	Vector3 bPos, bPos2;
	Vector3[] elecPos = new Vector3[3];

	Animator anim;
	int Attack1Hash = Animator.StringToHash("Attack1");
	int Attack2Hash = Animator.StringToHash("Attack2");
	int Attack3Hash = Animator.StringToHash("Attack3");
	//int UltraHash = Animator.StringToHash("Ultra");
	// Use this for initialization
	void Start () {
		comboTime1 = 0.0f;
		pMove = GetComponent<MoveTest> ();
		if (Application.loadedLevel == 1) {
			fire = wind = elec = false;
			ult1 = ult2 = ult3 = false;
		}
		anim = GetComponent<Animator> ();
		isAttacking = false;
		barrier.SetActive (false);
		//rhythm meter declaration
		p = GameObject.FindGameObjectWithTag ("Player");
		stats = p.GetComponent<PlayerStats> ();
		beatTrack = 0;
	}
	
	// Update is called once per frame
	void Update () {
		barrier.transform.position = new Vector3(transform.position.x, transform.position.y + .95f, transform.position.z);

		if (pMove.isRight == 1)
			dir = 1;
		if (pMove.isRight == -1)
			dir = -1;
		//change pick deterines which attack you're using
		ChangePick ();

		//sets the position for where the attack will spawn

		if (pick == 3)
			bPos = new Vector3 (transform.position.x + (4f * dir), transform.position.y + 5, transform.position.z);
		else
			bPos = new Vector3 (transform.position.x + (1.5f * dir), transform.position.y, transform.position.z);

		bPos2 = new Vector3 (transform.position.x + (1.5f * -dir), transform.position.y, transform.position.z);

		elecPos[0] = bPos;
		elecPos[1] = new Vector3 (transform.position.x + (6.5f * dir), transform.position.y + 5, transform.position.z);
		elecPos[2] = new Vector3 (transform.position.x + (9f * dir), transform.position.y + 5, transform.position.z);

		//if the combo time runs out, revert to normal state
		//destroy any lingering attack sprites if they're there
		if (Time.time > comboTime1) {
			combo = 0;
			isAttacking = false;
		}

		if (Input.GetKeyDown (KeyCode.X) || Input.GetKeyDown (KeyCode.JoystickButton3)){
			barrier.SetActive(true);
			isBlocking = true;
		}

		if (Input.GetKeyUp (KeyCode.X) || Input.GetKeyUp (KeyCode.JoystickButton3)){
			barrier.SetActive(false);
			isBlocking = false;
		}

		//first attack
		if ((Input.GetKeyDown (KeyCode.Z) || Input.GetKeyDown (KeyCode.JoystickButton2)) && Time.time > comboTime1 && combo == 0 && !isBlocking) {
			comboTime1 = 1;
			isAttacking = true;
			comboTime1 = Time.time + .6f;

			switch (pick){
			case 0:
				anim.SetTrigger (Attack1Hash);
				b = Instantiate (attacks[pick], bPos, attacks[pick].transform.rotation) as GameObject;
				b.transform.localScale = new Vector3 (b.transform.localScale.x * dir, b.transform.localScale.y, b.transform.localScale.z);
				break;
			case 1:
				anim.SetTrigger (Attack1Hash);
				b = Instantiate (attacks[pick], bPos, attacks[pick].transform.rotation) as GameObject;
				b.transform.localScale = new Vector3 (b.transform.localScale.x * dir, b.transform.localScale.y, b.transform.localScale.z);
				break;
			case 2:
				anim.SetTrigger (Attack2Hash);
				StartCoroutine (WindAttack(attacks[pick], bPos, bPos2));
				break;
			case 3:
				anim.SetTrigger (Attack3Hash);
				StartCoroutine (ElecAttack(attacks[pick], elecPos[0],0));
				break;
			default:
				break;
			}
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
		if (Input.GetKeyDown (KeyCode.Alpha4) && (ult1 && ult2 && ult3))
			pick = 4;
	}

	IEnumerator ElecAttack(GameObject bolt, Vector3 pos, int boltIndex) {
		yield return new WaitForSeconds (.2f);
		Instantiate (bolt, pos, bolt.transform.rotation);
		if (boltIndex == 2)
			yield break;
		StartCoroutine (ElecAttack(bolt, elecPos[boltIndex+1], boltIndex+1));
	}

	IEnumerator Attack(GameObject attack, Vector3 pos) {
		yield return new WaitForSeconds (.3f);
		Instantiate (attack, pos, attack.transform.rotation);
	}

	IEnumerator WindAttack(GameObject wind, Vector3 pos1, Vector3 pos2){
		yield return new WaitForSeconds(.3f); 
		GameObject dummy1 = Instantiate (wind, pos1, wind.transform.rotation) as GameObject;

		GameObject dummy2 = Instantiate (wind, pos2, wind.transform.rotation) as GameObject;

		dummy1.transform.localScale = new Vector3 (dummy1.transform.localScale.x * dir, dummy1.transform.localScale.y, dummy1.transform.localScale.z);
		dummy2.transform.localScale = new Vector3 (dummy2.transform.localScale.x * -dir, dummy2.transform.localScale.y, dummy2.transform.localScale.z);
		if (dir == 1) {
			dummy1.GetComponent<AttackAI> ().windR = true;
			dummy2.GetComponent<AttackAI> ().windL = true;
		}
		else{
			dummy1.GetComponent<AttackAI> ().windL = true;
			dummy2.GetComponent<AttackAI> ().windR = true;
		}
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
