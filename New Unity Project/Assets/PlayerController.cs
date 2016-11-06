using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public int moveSpeed = 50;
	public int jumpHeight = 500;
	public float dampener = 0.9f;
	public float raycastLenScaled = 0.75f;

	private float horizMoveCommand;
	private bool jumpCommand;

	Rigidbody2D rb2D;

	void Start () {
		rb2D = GetComponent<Rigidbody2D>();
	}

	bool grounded {
		get {
			int layerMask = 1 << LayerMask.NameToLayer("Terrain");
			RaycastHit2D raycast = Physics2D.Raycast (
				rb2D.position,
				Vector2.down,
				raycastLenScaled * Mathf.Max(
					transform.lossyScale.x,
					transform.lossyScale.y),
				layerMask);
			if (raycast.collider)
				return true;
			return false;
		}
	}

	/* Update methods: */

	void Update () {
		horizMoveCommand = Input.GetAxisRaw ("Horizontal");
		jumpCommand = Input.GetKeyDown (KeyCode.W);
	}

	/* FixedUpdate methods: */
	
	void FixedUpdate () {
		dampenSpeed();
		rb2D.AddForce(activeHorizontalForces());
		rb2D.AddForce(activeVerticalForces());
	}

	void dampenSpeed() {
		float horizVelocity = rb2D.velocity.x;
		horizVelocity *= dampener;
		rb2D.velocity = new Vector2(horizVelocity, rb2D.velocity.y);
	}

	Vector2 activeHorizontalForces () {
		Vector2 moveDir = new Vector2 (horizMoveCommand, 0);
		return moveDir * moveSpeed;
	}
	
	Vector2 activeVerticalForces() {
		if (jumpCommand && grounded) {
			return new Vector2 (0, jumpHeight);
		}
		return Vector2.zero;
	}
}
