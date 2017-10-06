using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerArrowKeysController : MonoBehaviour
{
	public float acceleration = 20;
	public float maxSpeed = 3;

	private Rigidbody2D playerRigidBody;
	private int count;

	void Start()
	{
		playerRigidBody = GetComponent<Rigidbody2D>();
		count = 0;
	}

	void Update () 
	{
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

		//if (velocity.magnitude > maxSpeed) {
		//	playerRigidBody.velocity = Vector2.ClampMagnitude (velocity, maxSpeed);
		//} else {
			playerRigidBody.AddForce (movement * acceleration);
		//}

		Vector2 moveDirection = playerRigidBody.velocity;
		if (moveDirection != Vector2.zero) {
			float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
