using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public float acceleration = 1f;
	public float maxSpeed = 0.1f;

	private Rigidbody2D playerRigidBody;
	private int count;

	void Start()
	{
		playerRigidBody = GetComponent<Rigidbody2D>();
		count = 0;
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Debug.Log (
			"x:"+playerRigidBody.velocity.x
			+"\ty:"+playerRigidBody.velocity.y
			+"\tmagnitude:"+playerRigidBody.velocity.magnitude
			+"\tnormalized:"+playerRigidBody.velocity.normalized
		);

		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		Vector2 velocity = playerRigidBody.velocity;
		Vector2 pos = transform.position;

		if (velocity.magnitude > maxSpeed) {
			playerRigidBody.velocity = Vector2.ClampMagnitude (velocity, maxSpeed);
		} else {
			playerRigidBody.AddForce (movement * acceleration);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("NotePickup")) {
			other.gameObject.SetActive (false);
			count++;
		}
	}
}
