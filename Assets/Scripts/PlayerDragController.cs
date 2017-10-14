using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDragController : MonoBehaviour
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
        Debug.DrawRay (transform.position, transform.rotation * Vector3.right, Color.red);

        float x = 0;
        float y = 0;
        bool isMoving = false;

        if (Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0) {
            // Disable drag if a key is pressed.
            // Keys have precedence.
            return;
        }

        if (Input.touchCount > 0) {
            // Screen touch
            Vector2 position = Input.GetTouch (1).position;
            x = position.x;
            y = position.y;
            isMoving = true;
        } else if (Input.GetMouseButton (0)) {
            // Left click
            Vector3 position = Input.mousePosition;
            x = position.x;
            y = position.y;
            isMoving = true;
        }

        if (isMoving) {
            Plane plane = new Plane (Vector3.back, transform.position);
            Ray ray = Camera.main.ScreenPointToRay (new Vector3 (x, y, 0f));
            float point = 0f;

            if (plane.Raycast (ray, out point)) {
                Vector3 targetPosition = ray.GetPoint (point);

                Vector2 currentMovement = (Vector2)(targetPosition - (Vector3)(playerRigidBody.position)).normalized;

                playerRigidBody.AddForce (currentMovement * acceleration);

                Vector2 velocity = playerRigidBody.velocity;
                Vector2 pos = transform.position;

                Vector2 moveDirection = playerRigidBody.velocity;
                if (moveDirection.magnitude > minSpeedToStop) {
                    float angle = Mathf.Atan2 (moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
                } else {
                    playerRigidBody.velocity = Vector2.zero;
                }
            }
        }	
    }
}
