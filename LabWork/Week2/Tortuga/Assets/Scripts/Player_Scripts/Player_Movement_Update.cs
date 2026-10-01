using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement_Update : MonoBehaviour
{
    private Rigidbody2D rb;
    private float shipSpeed = 5f;
    //sets the players speed - could have been made public/serialized so could be changed in inspector.
    private Vector2 movementInput;
    private Vector2 smoothedMovementInput;
    private Vector2 movementInputSmoothSpeed;
    private Camera Cam;
    [SerializeField]
    private float safeScreenBoundary;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //when scene loads, gets the players rigid body.
        Cam = Camera.main;
    }

    private void FixedUpdate()
    {
        smoothedMovementInput = Vector2.SmoothDamp(smoothedMovementInput, movementInput, ref movementInputSmoothSpeed,0.1f);
        rb.linearVelocity = smoothedMovementInput * shipSpeed;
        //This script means when moving has slight smoothness, means no tiny pizel movements that look odd to the eye.

        PreventShipGoingOffScreen();
    }
    private void PreventShipGoingOffScreen()
    {
        //script stops the player leaving the screen they can see. As looks ugly if player can just fly off
        Vector2 screenPosition = Cam.WorldToScreenPoint(transform.position);

        if ((screenPosition.x < safeScreenBoundary && rb.linearVelocityX < 0) || (screenPosition.x > Cam.pixelWidth - safeScreenBoundary && rb.linearVelocityX > 0))
        {
            rb.linearVelocity = new Vector2(0,rb.linearVelocityY);
        }
        if ((screenPosition.y < safeScreenBoundary && rb.linearVelocityY < 0) || (screenPosition.y > Cam.pixelHeight - safeScreenBoundary && rb.linearVelocityY > 0))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX,0);
        }
    }
    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
        //when a player moves, takes values and puts them into coordinates
    }

}
