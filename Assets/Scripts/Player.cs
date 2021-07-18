using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Acceleration and speed
    public float accel = 6f;
    public float speed = 10f;
    // Input and rigidbody
    private Vector2 input;
    private Rigidbody2D rb;
    // Jump stuff such as checks and speed
    public float jump = 14f;
    public bool isJumping;
    public float jumpSpeed = 8f;
    // Checking raycast for wall and jump
    private float rayCastLengthCheck = 0.005f;
    // Jump duration + threshold
    public float jumpDurationThreshold = 0.25f;
    private float jumpDuration;
    // Air acceleration
    public float airAccel = 3f;

    // The bounding box of the player
    private float width;
    private float height;

    private void Awake()
    {
        // Copying the component details into the variables
        rb = GetComponent<Rigidbody2D>();
        width = GetComponent<Collider2D>().bounds.extents.x + 0.1f;
        height = GetComponent<Collider2D>().bounds.extents.y + 0.2f;
    }


    // Start is called before the first frame update
    void Start()
    {

    }


    // Checks if the player is on the ground
    public bool PlayerIsOnGround()
    {
        // Checks if the player is on the ground in three separate scenarios
        // Checking ground
        bool groundCheck1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - height), -Vector2.up, rayCastLengthCheck);

        // Chekcing using width + -
        bool groundCheck2 = Physics2D.Raycast(new Vector2(transform.position.x + (width - 0.2f), transform.position.y - height), -Vector2.up, rayCastLengthCheck);
        bool groundCheck3 = Physics2D.Raycast(new Vector2(transform.position.x - (width - 0.2f), transform.position.y - height), -Vector2.up, rayCastLengthCheck);

        if (groundCheck1 || groundCheck2 || groundCheck3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Checks walls
    public bool IsWallToLeftOrRight()
    {
        // Checks if player is sliding down a wall
        bool wallOnleft = Physics2D.Raycast(new Vector2(transform.position.x - width, transform.position.y), -Vector2.right, rayCastLengthCheck);
        bool wallOnRight = Physics2D.Raycast(new Vector2(transform.position.x + width, transform.position.y), Vector2.right, rayCastLengthCheck);

        if (wallOnleft || wallOnRight)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Does a final check to check if they're on either ground or a wall
    public bool PlayerIsTouchingGroundOrWall()
    {
        if (PlayerIsOnGround() || IsWallToLeftOrRight())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Now does a check to determine the direction of said wall
    // This is to determine animation as well as controls
    public int GetWallDirection()
    {
        bool isWallLeft = Physics2D.Raycast(new Vector2(transform.position.x - width, transform.position.y), -Vector2.right, rayCastLengthCheck);
        bool isWallRight = Physics2D.Raycast(new Vector2(transform.position.x + width, transform.position.y), Vector2.right, rayCastLengthCheck);

        if (isWallLeft)
        {
            return -1;
        }
        else if (isWallRight)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    // not FixUpdate is called once per frame
    void Update()
    {
        // input copying horizontal to input.x, vice versa for .y axis
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        // checking if there's an input on the Y axis, and then adding the jump duration value
        if (input.y >= 1f)
        {
            jumpDuration += Time.deltaTime;
        }
        else
        {
            // sets player to ground and sets their jump duration to 0
            isJumping = false;
            jumpDuration = 0f;
        }

        // Checks to see if the player can jump
        if (PlayerIsOnGround() && !isJumping)
        {

            if (input.y > 0f)
            {
                isJumping = true;
            }
        }

        // This makes input y 0 if duration is past the threshold
        if (jumpDuration > jumpDurationThreshold) input.y = 0f;
    }

    void FixedUpdate()
    {
        // Accel variable
        var acceleration = 0f;

        // If player is on the ground, acceleration is equal to accel to gain acceleration.
        if (PlayerIsOnGround())
        {
            acceleration = accel;
        }
        else
        {
            // same but for air
            acceleration = airAccel;
        }

        // X velocity measure to move the player in either  direction
        var xVelocity = 0f;

        // Checks to see if the player is not moving & on ground to set velocity to 0
        if (PlayerIsOnGround() && input.x == 0)
        {
            xVelocity = 0f;
        }
        else
        {
            xVelocity = rb.velocity.x;
        }

        var yVelocity = 0f;

        // Now doing the velocity hijinks with jumping
        if (PlayerIsTouchingGroundOrWall() && input.y == 1)
        {
            yVelocity = jump;
        }
        else
        {
            yVelocity = rb.velocity.y;
        }
        // Jumping!
        rb.AddForce(new Vector2(((input.x * speed) - rb.velocity.x) * acceleration, 0));
        rb.velocity = new Vector2(xVelocity, yVelocity);

        // Getting the wall slide velocities.
        if (IsWallToLeftOrRight() && !PlayerIsOnGround() && input.y == 1)
        {
            rb.velocity = new Vector2(-GetWallDirection() * speed * 0.75f, rb.velocity.y);
        }

        if (isJumping && jumpDuration < jumpDurationThreshold)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }
}
