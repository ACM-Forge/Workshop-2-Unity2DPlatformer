using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D box;
    public Vector3 checkPoint;

    [Header("Stats")]
    public float Speed = 5.0f;
    public float AirControlModifier = 0.5f;
    public float JumpPower = 10.0f;

    [Header("States")]
    public bool Walking = false;
    public bool CanJump = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        ResetPlayer();
    }

    void Move() 
    {
        // Get the horizontal input from the player (A/D, <-,->)
        float horizontalMovement = Input.GetAxis("Horizontal"); 
        
        // Sync the movement speed with the framerate
        float moveForce = horizontalMovement * Speed * Time.deltaTime; 

         // If the player cannot jump -> the player is in the air
        if(!CanJump) {
            moveForce *= AirControlModifier;
        }
        rb.AddForce(Vector2.right * moveForce);

        // Flip sprite to face the correct direction based on the player input
        flipSprite(horizontalMovement); 
    }

    void Jump()
    {
        // If we are touching something and pressing space
        if (CanJump && Input.GetKey(KeyCode.Space)) 
        {
            rb.AddForce(Vector2.up * JumpPower,ForceMode2D.Impulse);
            CanJump = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        CanJump = true;
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        CanJump = true;
    }

    void ResetPlayer() 
    {
        // If we fall further than 10 units off the map
        if (transform.position.y < -10.0f) { 
            // Reset to checkpoint
            transform.position = checkPoint; 
        }
    }

    void flipSprite(float input)
    {
        // Moving Right
        if(input > 0)  
        {
            // Face sprite right
            float degrees = 0;
            transform.localRotation = Quaternion.Euler(0, degrees, 0); 
        }
        // Moving Left
        else if (input < 0)
        {
            // Face sprite left
            float degrees = 180;
            transform.localRotation = Quaternion.Euler(0, degrees, 0); 
        }
    }
}
