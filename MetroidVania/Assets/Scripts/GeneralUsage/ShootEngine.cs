using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEngine : MonoBehaviour
{
    [Header("Shoot attributes")]
    [SerializeField] GameObject shootPrefab;
    [SerializeField] float shootSpeed = 10f;

    Coroutine firingCoroutine;

    GameObject aimingArea;

    // Start is called before the first frame update
    void Start()
    {
        aimingArea = GameObject.Find("PlayerAim");
    }

    // Update is called once per frame
    void Update()
    {
        shoot();
    }


    private void shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuosly());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }



    IEnumerator FireContinuosly()
    {
        Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);
     Vector2 targetPosition = (TargetEngine.mousePosition - playerPosition).normalized;
     GameObject laser = Instantiate(shootPrefab,new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z)
                        , Quaternion.identity) as GameObject;

        laser.GetComponent<Rigidbody2D>().velocity += targetPosition* shootSpeed * Time.deltaTime;

       
     yield return new WaitForSeconds(0.5f);

    }

    

}
