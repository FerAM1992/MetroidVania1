using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets;
using UnityStandardAssets.CrossPlatformInput;

public class JumpImprovement : MonoBehaviour
{
    //Code from https://www.youtube.com/watch?v=7KiK0Aqtmzc
    [SerializeField] float fallingGravityMulti = 2.5f;
    [SerializeField] float lowJumpGravityMult = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            GetComponent<Rigidbody2D>().velocity += Vector2.up * Physics2D.gravity.y * (fallingGravityMulti - 1) * Time.deltaTime;
        }
        else if(GetComponent<Rigidbody2D>().velocity.y > 0 && !CrossPlatformInputManager.GetButton("Jump"))
        {
            GetComponent<Rigidbody2D>().velocity += Vector2.up * Physics2D.gravity.y * (lowJumpGravityMult - 1) * Time.deltaTime;
        }
    }
}
