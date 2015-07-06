using UnityEngine;
using System.Collections;

public class MoveTest : MonoBehaviour {
	public float speed;
	public float jPower;
	public float flipLeft, flipRight;
	public int isRight;
	public CharacterController controller;
	public PlayerStats pStats;
	public PlayerAttack pAttack;
	public Vector3 movement = Vector3.zero;
	public Vector3 checkpoint;
	//turning player
	//bool isRotated;

	//animation setup
	Animator anim;
	int HitHash = Animator.StringToHash("Hit");
	int JumpHash = Animator.StringToHash("Jump");

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();

		pStats = GetComponent<PlayerStats> ();
		pAttack = GetComponent<PlayerAttack> ();
		controller = GetComponent<CharacterController> ();
		isRight = 1;
		flipRight = transform.localScale.x;
		flipLeft = -flipRight;

		checkpoint = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		float run = Mathf.Abs(Input.GetAxis("Horizontal"));
		float runY = Mathf.Abs(Input.GetAxis("Vertical"));
		anim.SetFloat("IsMoving", run);
		anim.SetFloat("IsMovingY", runY);

		if (Input.GetAxis ("Horizontal") < 0) {
			isRight = -1;
			transform.localScale = new Vector3 (flipLeft, transform.localScale.y, transform.localScale.z);
		}
		if (Input.GetAxis ("Horizontal") > 0) {
			isRight = 1;
			transform.localScale = new Vector3 (flipRight, transform.localScale.y, transform.localScale.z);
		}
		movement.x = Input.GetAxis ("Horizontal") * speed;
		movement.z = Input.GetAxis ("Vertical") * speed;

		if (pAttack.isAttacking)
			movement = Vector3.zero;

		//gravity code
		if (controller.isGrounded == false) 
			movement.y -= 35f * Time.deltaTime;

		if (Input.GetButton ("Jump") && controller.isGrounded == true) {
			movement.y = jPower;
			anim.SetTrigger (JumpHash);
		}
		//move the player based on the movement vector
		controller.Move (movement * Time.deltaTime);
	}

	void OnControllerColliderHit (ControllerColliderHit hit){
		//if the player runs into an enemy
		//bounce the player away in the opposite direction
		if (hit.collider.gameObject.tag == "Enemy") {
			//movement = new Vector3 (-hit.moveDirection.x * 6.0f, 3.0f, 0.0f);
			pStats.GetHit (1);
			anim.SetTrigger (HitHash);
		}
		if (hit.collider.gameObject.tag == "EnemyBullet") {
			//movement = new Vector3 (-hit.moveDirection.x * 4.0f, 2.0f, 0.0f);
			pStats.GetHit (1);
			anim.SetTrigger (HitHash);
		}
	}
}
