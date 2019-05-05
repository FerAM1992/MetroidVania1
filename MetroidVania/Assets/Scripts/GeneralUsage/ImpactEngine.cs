using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEngine : MonoBehaviour
{

    [SerializeField] int damge = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("enemy"))
        {
            int currentLife = collision.GetComponent<DamageDealer>().getLife();
            collision.GetComponent<DamageDealer>().setLife(currentLife - damge);
        }
    }
}
