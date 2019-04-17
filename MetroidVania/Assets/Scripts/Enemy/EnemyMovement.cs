using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    float direction = 1;
    [SerializeField] float movementSpeed = 2f;
    bool stoppedTouchingGround = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        
    }


    private void Move()
    {
        if (stoppedTouchingGround)
        {
            Vector2 moveVelocity = new Vector2(movementSpeed * direction, GetComponent<Rigidbody2D>().velocity.y);
            GetComponent<Rigidbody2D>().velocity = moveVelocity;
            if (GetComponent<Animator>().GetBool("isTouchingGround")) {
                stoppedTouchingGround = false;
            }
        }
        else {
            if (GetComponent<Animator>().GetBool("isTouchingGround"))
            {
                Vector2 moveVelocity = new Vector2(movementSpeed * direction, GetComponent<Rigidbody2D>().velocity.y);
                GetComponent<Rigidbody2D>().velocity = moveVelocity;
            }
            else
            {
                flipPlayerSprite();
                direction *= -1f;
                stoppedTouchingGround = true;
            }
        }
        
        
    }


    private void flipPlayerSprite()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1,transform.localScale.y);
    }
}
