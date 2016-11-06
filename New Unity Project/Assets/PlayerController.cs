using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public int moveSpeed;
	public int jumpHeight;

	Rigidbody2D rb2D;

	void Start () {
		rb2D = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		Vector2 moveDir = new Vector2 (Input.GetAxisRaw ("Horizontal") * moveSpeed, rb2D.velocity.y);
		rb2D.velocity = moveDir;
		if (Input.GetKeyDown (KeyCode.W)) {
			rb2D.AddForce (new Vector2 (0, jumpHeight));
		}
	}
}
