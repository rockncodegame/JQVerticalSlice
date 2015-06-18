using UnityEngine;
using System.Collections;

public class PlayerBGScroll : MonoBehaviour {

	//Variables
	public GameObject p;
	public float scrollSpeed;
	private Vector2 savedOffset;
	
	// Setting Player and the base offset to reset when play stops
	void Start () {
		p = GameObject.Find ("Player");
		savedOffset = renderer.sharedMaterial.GetTextureOffset ("_MainTex");
	}
	
	// Generates float based of player transform and a flat speed
	// Then applies that float to a new offset and applies that to the texture
	void Update () {
		float x = Mathf.Repeat (p.transform.position.x * scrollSpeed, 1);
		Vector2 offset = new Vector2 (x, 0);
		renderer.sharedMaterial.SetTextureOffset ("_MainTex", offset);
	}

	//resets texture when play is disabled, otherwise offset stays at value it has when stopped
	void OnDisable (){
		renderer.sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
	}
}
