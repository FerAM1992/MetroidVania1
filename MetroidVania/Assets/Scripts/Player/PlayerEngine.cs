using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerEngine : MonoBehaviour {

    //Config
    [Header("Player Attributes")]
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float climbingSpeed = 3.5f;
    [SerializeField] float maximumJumpVelocity = 7.5f;


    //State
    bool isAlive = true;
    bool jumped = false;
    bool canJump = true;
    bool wallRayChecker;
    bool groundRayChecker;

    //cached component references
    Animator playerAnimator;
    float initialGravityScale;


    [SerializeField] float fallingGravityMulti = 2.5f;

    // Use this for initialization
    void Start () {
        playerAnimator = GetComponent<Animator>();
        initialGravityScale = GetComponent<Rigidbody2D>().gravityScale;
    }
	
	// Update is called once per frame
	void Update () {
        checkGround();
        Move();
        jump();
        flipPlayerSprite();
        climbLadder();
    }


    private void Move()
    {
        float flowControl = CrossPlatformInputManager.GetAxis("Horizontal") * movementSpeed;
        Vector2 moveVelocity = new Vector2(flowControl, GetComponent<Rigidbody2D>().velocity.y);
        GetComponent<Rigidbody2D>().velocity = moveVelocity;
        if (groundRayChecker) {
            playerAnimator.SetBool("Running",
            Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > Mathf.Epsilon);
        } else if (GetComponent<Rigidbody2D>().velocity.x == 0) {
            playerAnimator.SetBool("Running",false);
        }
    }



    private void jump() {
       

         if (groundRayChecker
            || GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Ladder")))
            {
                if (CrossPlatformInputManager.GetButtonDown("Jump"))
                {
                    if (GetComponent<Rigidbody2D>().velocity.y <= maximumJumpVelocity)
                    {
                        GetComponent<Rigidbody2D>().velocity += Vector2.up * jumpForce;
                    }
                    jumped = true;
                }
                playerAnimator.SetBool("Jumping", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) > Mathf.Epsilon);
            }
        
        
    }

    private void flipPlayerSprite() {
        bool playerHasHorizontalSpeed = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > Mathf.Epsilon; // this means that of the player moves no matter which side
        if (playerHasHorizontalSpeed) {
            transform.localScale = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x),1f); // this is taking the direction of the movement and fliping the sprite accordingly
        }
    }


    private void climbLadder()
    {

        if (!GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            playerAnimator.SetBool("Climbing", false);
            jumped = false;
            GetComponent<Rigidbody2D>().gravityScale = initialGravityScale;
            return;
        }
        if (!CrossPlatformInputManager.GetButton("Jump") && !jumped)
        {
            playerAnimator.SetBool("Jumping", false);
            GetComponent<Rigidbody2D>().gravityScale = 0f;
            float flowControl = CrossPlatformInputManager.GetAxis("Vertical") * climbingSpeed;
            Vector2 climbVelocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, flowControl);
            GetComponent<Rigidbody2D>().velocity = climbVelocity;
            playerAnimator.SetBool("Climbing", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) > Mathf.Epsilon);
        }
    }

    

    private void checkGround()
    {
        wallRayChecker = playerAnimator.GetBool("RayTouchingWall") ;
        groundRayChecker = playerAnimator.GetBool("RayIsTouchingGround");
        playerAnimator.SetBool("Iddling", GetComponent<Rigidbody2D>().velocity.Equals(new Vector2(0, 0)));
        //print(GetComponent<Rigidbody2D>().velocity); 
    }


    

 




}
