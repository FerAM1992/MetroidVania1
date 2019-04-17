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
        GetComponent<Rigidbody2D>().velocity = new Vector2(movementSpeed * direction, GetComponent<Rigidbody2D>().velocity.y);
        if (stoppedTouchingGround)
        {
          stoppedTouchingGround = !GetComponent<Animator>().GetBool("isTouchingGround");   
        }
        else {
            if (!GetComponent<Animator>().GetBool("isTouchingGround"))
            {
                flipPlayerSprite();
                stoppedTouchingGround = true;
            }
        }
        
        
    }


    private void flipPlayerSprite()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1,transform.localScale.y);
        direction *= -1f;
    }
}
