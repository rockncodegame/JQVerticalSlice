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
	public bool isAttacking, isBlocking, ultPart1;
	public GameObject[] attacks;
	//update rhythm meter
	public GameObject p;
	public PlayerStats stats;
	private int beatTrack;

	Vector3 bPos, bPos2;
	Vector3[] elecPos = new Vector3[3];
	Vector3[] ultPos = new Vector3[6];

	Animator anim;
	int AttackHash = Animator.StringToHash("Attack");
	int ShieldHash = Animator.StringToHash("Shield");
	//so shield doesnt spam
	public bool shieldUp;
	int UltraHash = Animator.StringToHash("Ultra");
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
		barrier.transform.position = new Vector3(transform.position.x, transform.position.y + .15f, transform.position.z);

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

		ultPos[0] = new Vector3 (transform.position.x + (4f), transform.position.y + 5, transform.position.z);
		ultPos[1] = new Vector3 (transform.position.x + (6.5f), transform.position.y + 5, transform.position.z);
		ultPos[2] = new Vector3 (transform.position.x + (9f), transform.position.y + 5, transform.position.z);
		ultPos[3] = new Vector3 (transform.position.x - (4f), transform.position.y + 5, transform.position.z);
		ultPos[4] = new Vector3 (transform.position.x - (6.5f), transform.position.y + 5, transform.position.z);
		ultPos[5] = new Vector3 (transform.position.x - (9f), transform.position.y + 5, transform.position.z);

		//if the combo time runs out, revert to normal state
		//destroy any lingering attack sprites if they're there
		if (Time.time > comboTime1) {
			combo = 0;
			isAttacking = false;
		}

		if (Input.GetKeyDown (KeyCode.X) || Input.GetKeyDown (KeyCode.JoystickButton3)){
			barrier.SetActive(true);
			isBlocking = true;

			if (shieldUp == false){
				shieldUp = true;
				anim.SetTrigger (ShieldHash);
			}
		}

		if (Input.GetKeyUp (KeyCode.X) || Input.GetKeyUp (KeyCode.JoystickButton3)){
			barrier.SetActive(false);
			isBlocking = false;

			if (shieldUp == true){
				shieldUp = false;
			}
		}

		//first attack
		if ((Input.GetKeyDown (KeyCode.Z) || Input.GetKeyDown (KeyCode.JoystickButton2)) && Time.time > comboTime1 && combo == 0 && !isBlocking) {
			isAttacking = true;
			comboTime1 = Time.time + .6f;

			switch (pick){
			case 0:
				anim.SetTrigger (AttackHash);
				b = Instantiate (attacks[pick], bPos, attacks[pick].transform.rotation) as GameObject;
				b.transform.localScale = new Vector3 (b.transform.localScale.x * dir, b.transform.localScale.y, b.transform.localScale.z);
				break;
			case 1:
				anim.SetTrigger (AttackHash);
				b = Instantiate (attacks[pick], bPos, attacks[pick].transform.rotation) as GameObject;
				b.transform.localScale = new Vector3 (b.transform.localScale.x * dir, b.transform.localScale.y, b.transform.localScale.z);
				break;
			case 2:
				anim.SetTrigger (AttackHash);
				StartCoroutine (WindAttack(attacks[pick], bPos, bPos2, .2f));
				break;
			case 3:
				anim.SetTrigger (AttackHash);
				StartCoroutine (ElecAttack(attacks[pick], elecPos[0],0));
				break;
			case 4:
				anim.SetTrigger (AttackHash);
				StartCoroutine ("Ultra");
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
		if ((Input.GetKeyDown (KeyCode.Alpha1) || Mathf.Round (Input.GetAxis ("Horizontal2")) < 0) && fire)
			pick = 1;
		if ((Input.GetKeyDown (KeyCode.Alpha2) || Mathf.Round (Input.GetAxis ("Vertical2")) > 0) && wind)
			pick = 2;
		if ((Input.GetKeyDown (KeyCode.Alpha3)|| Mathf.Round (Input.GetAxis ("Horizontal2")) > 0) && elec)
			pick = 3;
		if ((Input.GetKeyDown (KeyCode.Alpha4) || Mathf.Round (Input.GetAxis ("Vertical2")) < 0) && (ult1 && ult2 && ult3))
			pick = 4;
		if (Input.GetKeyDown (KeyCode.JoystickButton6) || Input.GetKeyDown (KeyCode.C))
			pick = 0;
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

	IEnumerator Ultra(){
		yield return new WaitForSeconds (0);
		ultPart1 = true;
		GameObject dummy1 = Instantiate (attacks [5], ultPos [0], attacks [5].transform.rotation) as GameObject;
		dummy1.GetComponent<AttackAI> ().ultDown = true;
		GameObject dummy2 = Instantiate (attacks [5], ultPos [1], attacks [5].transform.rotation) as GameObject;
		dummy2.GetComponent<AttackAI> ().ultDown = true;
		GameObject dummy3 = Instantiate (attacks [5], ultPos [2], attacks [5].transform.rotation) as GameObject;
		dummy3.GetComponent<AttackAI> ().ultDown = true;
		GameObject dummy4 = Instantiate (attacks [5], ultPos [3], attacks [5].transform.rotation) as GameObject;
		dummy4.GetComponent<AttackAI> ().ultDown = true;
		GameObject dummy5 = Instantiate (attacks [5], ultPos [4], attacks [5].transform.rotation) as GameObject;
		dummy5.GetComponent<AttackAI> ().ultDown = true;
		GameObject dummy6 = Instantiate (attacks [5], ultPos [5], attacks [5].transform.rotation) as GameObject;
		dummy6.GetComponent<AttackAI> ().ultDown = true;
		
		StartCoroutine (WindAttack (attacks [4], bPos, bPos2, 1f));
	}

	IEnumerator WindAttack(GameObject wind, Vector3 pos1, Vector3 pos2, float delay){
		yield return new WaitForSeconds(delay); 
		GameObject dummy1 = Instantiate (wind, pos1, wind.transform.rotation) as GameObject;

		GameObject dummy2 = Instantiate (wind, pos2, wind.transform.rotation) as GameObject;

		ultPart1 = false;

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
