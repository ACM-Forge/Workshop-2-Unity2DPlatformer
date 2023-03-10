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
		CheckIfFalling();
    }

	void CheckIfFalling(){
		bool isFalling = !isGrounded && (rb.velocity.y < 0);
		animator.SetBool("Falling", isFalling);
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

		// Animation
		animator.SetBool("Run", horizontalMovement != 0);
    }

    void Jump()
    {
        // If we are touching something and pressing space
        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) 
        {
            rb.AddForce(Vector2.up * jumpPower,ForceMode2D.Impulse);
			animator.SetTrigger("Jump");
            isGrounded = false;
			animator.SetBool("Grounded", false);
        }
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        isGrounded = true;
		animator.SetBool("Grounded", true);
    }

	void OnCollisionExit2D(Collision2D col) 
    {
		isGrounded = false;
        animator.SetBool("Grounded", false);
    }

    void ResetPlayer() 
    {
        // If we fall further than 10 units off the map
        if (transform.position.y < -10.0f) { 
            // Reset to checkpoint
            transform.position = checkPoint; 
        }
    }

}
