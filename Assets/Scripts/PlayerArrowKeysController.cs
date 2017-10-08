using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerArrowKeysController : MonoBehaviour
{
    public float acceleration = 20;
    public float maxSpeed = 3;
    public float minSpeedToStop = 0.1f;

    private Rigidbody2D playerRigidBody;

    void Start ()
    {
        playerRigidBody = GetComponent<Rigidbody2D> ();
    }

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Debug.Log ("h:" + moveHorizontal + "\tv:" + moveVertical);

        Vector2 movement = new Vector2 (moveHorizontal, moveVertical).normalized;
        Vector2 velocity = playerRigidBody.velocity;
        Vector2 pos = transform.position;

        playerRigidBody.AddForce (movement * acceleration);

        Vector2 moveDirection = playerRigidBody.velocity;
        if (moveDirection.magnitude > minSpeedToStop) {
            float angle = Mathf.Atan2 (moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
        } else {
            playerRigidBody.velocity = Vector2.zero;
        }
    }
}
