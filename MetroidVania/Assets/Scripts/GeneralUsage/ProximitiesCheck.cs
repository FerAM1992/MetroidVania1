using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximitiesCheck : MonoBehaviour
{

    [Header("Rays Configuration")]
    [SerializeField] int maxNumberOfGroundRays = 4;
    [SerializeField] float wallRayMaxLength = 0.7f;
    [SerializeField] float groundRayMaxLength = 0.63f;
    [SerializeField] bool needMoreThanOneRayToTouchground = false;



    // Update is called once per frame
    void FixedUpdate()
    {
        checkProximities();
    }

    private void checkProximities()
    {
        //Horizontal ray casting checking
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right), Color.yellow);     
        GetComponent<Animator>().SetBool("isTouchingWall", Physics2D.Raycast(transform.position, determineVerticalRayOrientation(),
            wallRayMaxLength, 1 << LayerMask.NameToLayer("Ground")));
        GetComponent<Animator>().SetBool("isTouchingGround", dynamicVerticalRayGenerator());
    }

    private bool dynamicVerticalRayGenerator() {
        bool isTouchingGround = needMoreThanOneRayToTouchground;
        float maxX = GetComponent<BoxCollider2D>().bounds.min.x;
        float maxY = GetComponent<BoxCollider2D>().bounds.min.y;
        float raySeparation = (1 - .001f) / maxNumberOfGroundRays;
        for (int i = 0; i < maxNumberOfGroundRays; i++) {
            Vector2 currentRayPosition = new Vector2(maxX + raySeparation, maxY);
            Debug.DrawRay(currentRayPosition, transform.TransformDirection(Vector2.down), Color.blue);
            if (needMoreThanOneRayToTouchground)
            {
                isTouchingGround = isTouchingGround &&
                Physics2D.Raycast(currentRayPosition, transform.TransformDirection(Vector2.down), groundRayMaxLength,
                1 << LayerMask.NameToLayer("Ground"));
            }
            else {
                isTouchingGround = isTouchingGround ||
                Physics2D.Raycast(currentRayPosition, transform.TransformDirection(Vector2.down), groundRayMaxLength,
                1 << LayerMask.NameToLayer("Ground"));
            }
            
            maxX = maxX + raySeparation;
        }
        return isTouchingGround;
    }

    private Vector2 determineVerticalRayOrientation() {
        if (transform.localScale.x == 1)
        {
            return transform.TransformDirection(Vector2.right);
        }
        else
        {
            return transform.TransformDirection(Vector2.left);
        }
    }
}
