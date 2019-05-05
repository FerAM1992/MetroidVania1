using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] public int life = 100;
    public int getLife() { return life; }
    public void setLife(int life ) { this.life = life; }

    private void Update()
    {
        if (life <= 0) {
            Destroy(gameObject);
        }
    }
}
