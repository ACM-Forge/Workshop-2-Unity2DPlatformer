using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D box;


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
    }

    void Move() 
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float moveForce = horizontalMovement * Speed * Time.deltaTime;
        if(!CanJump) {
            moveForce *= AirControlModifier;
        }
        rb.AddForce(Vector2.right * moveForce);
        Debug.Log("Moving by " + moveForce);
    }

    void Jump()
    {
        if (CanJump && Input.GetKeyDown(KeyCode.Space))
        {
            float jumpForce = JumpPower;
            rb.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
            CanJump = false;
        }
    }

    void ResetJump() 
    {
        CanJump = true;
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        ResetJump();
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        ResetJump();
    }
}
