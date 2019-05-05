using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEngine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("enemy"))
        {
            print("YOU GOT ME");
        }
    }
}
