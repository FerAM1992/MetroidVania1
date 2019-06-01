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
    bool wallRayChecker;
    bool groundRayChecker;
    bool aim45;
    bool aim315;

    //cached component references
    float initialGravityScale;

    // Use this for initialization
    void Start () {
   
        initialGravityScale = GetComponent<Rigidbody2D>().gravityScale;
    }
	
	// Update is called once per frame
	void Update () {
        checkGround();
        AimingPosition();
        Move();
        jump();
        flipPlayerSprite();
    }


    private void Move()
    {
        float flowControl = CrossPlatformInputManager.GetAxis("Horizontal") * movementSpeed;
        Vector2 moveVelocity = new Vector2(flowControl, GetComponent<Rigidbody2D>().velocity.y);
        GetComponent<Rigidbody2D>().velocity = moveVelocity;
        if (groundRayChecker) {
            GetComponent<Animator>().SetBool("Running",
            Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > Mathf.Epsilon);
        } else if (GetComponent<Rigidbody2D>().velocity.x == 0) {
            GetComponent<Animator>().SetBool("Running",false);
        }
    }



    private void jump() {
       

         if (groundRayChecker)
            {
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                if (GetComponent<Rigidbody2D>().velocity.y <= maximumJumpVelocity)
                {
                    GetComponent<Rigidbody2D>().velocity += Vector2.up * jumpForce;
                }
                jumped = true;
            }  
            }
        GetComponent<Animator>().SetBool("Jumping", !groundRayChecker);

    }

    private void flipPlayerSprite() {
        bool playerHasHorizontalSpeed = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > Mathf.Epsilon; // this means that of the player moves no matter which side
        if (playerHasHorizontalSpeed) {
            transform.localScale = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x),1f); // this is taking the direction of the movement and fliping the sprite accordingly
        }
    }


    private void AimingPosition() {
        aim45 = CrossPlatformInputManager.GetAxis("Aim45") > 0;
        aim315 = CrossPlatformInputManager.GetAxis("Aim315") > 0;
        if (!aim45 || !aim315) {
            GetComponent<Animator>().SetBool("Aim315", aim315);
            GetComponent<Animator>().SetBool("Aim45", aim45);
        }
    }

    

    private void checkGround()
    {
        wallRayChecker = GetComponent<Animator>().GetBool("isTouchingWall") ;
        groundRayChecker = GetComponent<Animator>().GetBool("isTouchingGround");
        bool isIddling = !GetComponent<Animator>().GetBool("Jumping") && !GetComponent<Animator>().GetBool("Running")
            && groundRayChecker;
        GetComponent<Animator>().SetBool("Iddling", isIddling);
        GetComponent<Animator>().SetFloat("VerticalVelocity", GetComponent<Rigidbody2D>().velocity.y);
    }


    

 




}
