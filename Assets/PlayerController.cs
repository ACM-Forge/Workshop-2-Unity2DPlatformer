using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
	private Animator animator;
	private SpriteRenderer spriteRenderer;
    public Vector3 checkPoint;

    [Header("Stats")]
    public float speed = 200.0f;
    public float airControlModifier = 0.5f;
    public float jumpPower = 100.0f;

    [Header("States")]
    public bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        ResetPlayer();
        SetAnimation();
    }

    void Move() 
    {
        // Get the horizontal input from the player (A/D, <-,->)
        float horizontalMovement = Input.GetAxis("Horizontal"); 
        
        // Sync the movement speed with the framerate
        float moveForce = horizontalMovement * speed * Time.deltaTime; 

         // If the player cannot jump -> the player is in the air
        if(!isGrounded) {
            moveForce *= airControlModifier;
        }
        rb.AddForce(Vector2.right * moveForce, ForceMode2D.Impulse);

        // Flip sprite to face the correct direction based on the player input
        spriteRenderer.flipX = horizontalMovement < 0;
    }

    void Jump()
    {
        // If we are touching something and pressing space
        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) 
        {
            rb.AddForce(Vector2.up * jumpPower,ForceMode2D.Impulse);
			animator.SetTrigger("Jump");
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        isGrounded = true;
    }

    void ResetPlayer() 
    {
        // If we fall further than 10 units off the map
        if (transform.position.y < -10.0f) { 
            // Reset to checkpoint
            transform.position = checkPoint; 
        }
    }

    void SetAnimation() {
        if (isGrounded) // We are on the ground
        {
            animator.SetBool("Grounded", true);
            if (rb.velocity.x > 1 || rb.velocity.x < -1) // We are moving on the ground
            {
                animator.SetBool("Run", true);
            }
            else { // We are not moving on the ground
                animator.SetBool("Run", false);
            }
            
        }
        else // We are not on the ground
        {
            animator.SetBool("Grounded", false);
            animator.SetBool("Falling", rb.velocity.y < 0);  //... and falling
            
        }
    }

}
