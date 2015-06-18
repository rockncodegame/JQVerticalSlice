using UnityEngine;
using System.Collections;

public class PlayerAttackVSU : MonoBehaviour, IVisModifierTarget {
	//declaring variables
	public GameObject bulletSource;
	public GameObject b, b2, b3;
	public float comboTime1;
	public static bool fire, wind, elec;
	public static int ult = 0;
	public int combo, pick, dir;
	public MoveTest pMove;
	public GameObject playr;
	public PlayerStats stats;
	private int beatTrack;
	
	// Use this for initialization
	void Start () {
		comboTime1 = 0.0f;
		combo = 0;
		pMove = GetComponent<MoveTest> ();
		fire = wind = elec = false;
		playr = GameObject.FindGameObjectWithTag ("Player");
		stats = playr.GetComponent<PlayerStats> ();
		beatTrack = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
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
		}
		
		//first attack
		if (Input.GetKeyDown (KeyCode.Z) && Time.time > comboTime1 && combo == 0) {
			combo = 1;
			comboTime1 = Time.time + 1.0f;
			GetComponent<SpriteRenderer>().color = Color.magenta;
			b = Instantiate (bulletSource, bPos, Quaternion.identity) as GameObject;
			ChangeAttack (b);
		}
		//if the player continues the combo before the time is used, do the next move
		//if the first attack sprite is still there, destroy it
		//change the player state
		//create the new attack and then change its color based on the key
		//scale the attack up to signify a bigger attack
		else if (combo == 1 && Time.time < comboTime1 && Input.GetKeyDown (KeyCode.Z)){
			if (b != null)
				Destroy (b);
			combo = 2;
			GetComponent<SpriteRenderer>().color = Color.white;
			b2 = Instantiate (bulletSource, bPos, Quaternion.identity) as GameObject;
			ChangeAttack (b2);
			b2.transform.localScale = new Vector3 (b2.transform.localScale.x + 2, b2.transform.localScale.y + 2, b2.transform.localScale.z);
		}
		//if the player continues the combo before the time is used, do the next move
		//if the first attack sprite is still there, destroy it
		//change the player state
		//create the new attack and then change its color based on the key
		//scale the attack up to signify a bigger attack
		else if (combo == 2 && Time.time < comboTime1 && Input.GetKeyDown (KeyCode.Z)){
			if (b2 != null)
				Destroy (b2);
			combo = 3;
			GetComponent<SpriteRenderer>().color = Color.yellow;
			b3 = Instantiate (bulletSource, bPos, Quaternion.identity) as GameObject;
			ChangeAttack (b3);
			b3.transform.localScale = new Vector3 (b3.transform.localScale.x + 4, b3.transform.localScale.y + 4, b3.transform.localScale.z);
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
	
	//change color based on the pick selected
	//1 = fire (red)
	//2 = wind (green)
	//3 = electricity (light blue)
	//4 = ultimate (black)
	public void ChangeAttack(GameObject g){
		if (pick == 1)
			g.GetComponent<SpriteRenderer>().color = Color.red;
		if (pick == 2)
			g.GetComponent<SpriteRenderer>().color = Color.green;
		if (pick == 3)
			g.GetComponent<SpriteRenderer>().color = Color.cyan;
		if (pick == 4)
			g.GetComponent<SpriteRenderer>().color = Color.black;
	}
	
	public void OnValueUpdated(float current, float previous, float difference, float adjustedDifference){
		if (adjustedDifference > 0.1 && combo>0) {
			Debug.Log("Attacked On Beat");
			stats.rhythm += 1;
		}
		if (adjustedDifference > 0.1) {
			beatTrack++;
			Debug.Log("Beat " + beatTrack);
		}
	}
}