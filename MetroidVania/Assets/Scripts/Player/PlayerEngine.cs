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
    [SerializeField] float jumpSpeed = 5f;

    //State
    bool isAlive = true;

    //cached component references
    Animator playerAnimator;


    // Use this for initialization
    void Start () {
        playerAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        Move();
        jump();
        flipPlayerSprite();
    }


    private void Move()
    {
        float flowControl = CrossPlatformInputManager.GetAxis("Horizontal") * movementSpeed;
        Vector2 playerVelocity = new Vector2(flowControl, GetComponent<Rigidbody2D>().velocity.y);
        GetComponent<Rigidbody2D>().velocity = playerVelocity;
        playerAnimator.SetBool("Running", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > Mathf.Epsilon);
    }



    private void jump() {
        Vector2 jumpVector = new Vector2(0f, jumpSpeed);
        if (CrossPlatformInputManager.GetButton("Jump")) {
            GetComponent<Rigidbody2D>().velocity += jumpVector;
        }
    }

    private void flipPlayerSprite() {
        bool playerHasHorizontalSpeed = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > Mathf.Epsilon; // this means that of the player moves no matter which side
        if (playerHasHorizontalSpeed) {
            transform.localScale = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x),1f); // this is taking the direction of the movement and fliping the sprite accordingly
        }
    }
    

}
