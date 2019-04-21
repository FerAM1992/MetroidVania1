using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootShreder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Shoot"))
        {
            Destroy(collision.gameObject);
        }

    }
}
