using UnityEngine;
using System.Collections;

public class ConstantBGScroll : MonoBehaviour {

	// All necessary variables
	public GameObject p;
	public float scrollSpeed;
	public float scrollChangeRate;
	private float baseSpeed;
	private Vector2 savedOffset;
	private float oldX;
	
	// Sets player base offset, base speed, and the current player position
	void Start () {
		p = GameObject.Find ("Player");
		savedOffset = renderer.sharedMaterial.GetTextureOffset ("_MainTex");
		oldX = p.transform.position.x;
		baseSpeed = scrollSpeed;
	}
	
	// If the player moves Right, increases speed
	// If the player moves Left, reduces speed
	// Otherwise, resets scroll speed to 0
	// Then takes the current speed and scrolls in relation to time
	// Sets offset vector and applies it to the texture
	void Update () {
		if (oldX < p.transform.position.x) {
			if (scrollSpeed < baseSpeed * 1.5f){
				scrollSpeed += scrollChangeRate;
				oldX = p.transform.position.x;
			}
			else if (scrollSpeed >= baseSpeed * 1.5f){
				scrollSpeed = baseSpeed * 1.5f;
				oldX = p.transform.position.x;
			}
		} 
		else if (oldX > p.transform.position.x) {
			if (scrollSpeed > baseSpeed * 0.75f){
				scrollSpeed -= scrollChangeRate;
				oldX = p.transform.position.x;
			}
			else if (scrollSpeed <= baseSpeed * 0.75f){
				scrollSpeed = baseSpeed * 0.75f;
				oldX = p.transform.position.x;
			}
		}
		else if (oldX == p.transform.position.x) {
			if (scrollSpeed > baseSpeed){
				scrollSpeed -= scrollChangeRate;
			}
			else if (scrollSpeed < baseSpeed){
				scrollSpeed += scrollChangeRate;
			}
			else if (scrollSpeed == baseSpeed){
				scrollSpeed = baseSpeed;
			}
		}
		
		float x = Mathf.Repeat (Time.time * scrollSpeed, 1);
		Vector2 offset = new Vector2 (x, 0);
		renderer.sharedMaterial.SetTextureOffset ("_MainTex", offset);
	}

	// Resets Texture offset on play stop, otherwise it will stay at offset when the play stopped.
	void OnDisable (){
		renderer.sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
	}
}
