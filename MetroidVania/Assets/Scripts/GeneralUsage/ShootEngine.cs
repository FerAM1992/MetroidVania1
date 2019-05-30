using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEngine : MonoBehaviour
{
    [Header("Shoot attributes")]
    [SerializeField] GameObject shootPrefab;
    [SerializeField] float shootSpeed = 10f;
    
    public static bool isAiming = false;
    Coroutine firingCoroutine;

    GameObject aimingArea;

    // Start is called before the first frame update
    void Start()
    {
        aimingArea = GameObject.Find("PlayerAim");
        aimingArea.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        shoot();
    }


    private void shoot()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && GetComponent<Animator>().GetBool("isTouchingGround")) {
            aimingArea.active = true;
            isAiming = true;
            GetComponent<Animator>().SetBool("Running", false);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        } else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            aimingArea.active = false;
            isAiming = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireToMousePosition());
            if (GetComponent<Animator>().GetBool("Iddling")) {
                GetComponent<Animator>().SetBool("IddleShooting", true);
            }
            
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
            GetComponent<Animator>().SetBool("IddleShooting", false);
        }
    }



    IEnumerator FireToMousePosition()
    {
     Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);
        GameObject laser = Instantiate(shootPrefab, 
            new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z)
            , Quaternion.identity) as GameObject;
        Vector2 targetPosition;
        if (isAiming)
        {
            targetPosition = (TargetEngine.mousePosition - playerPosition).normalized;
        }
        else {
            targetPosition = new Vector2(transform.localScale.x, 0);
        }
           
     
    laser.GetComponent<Rigidbody2D>().velocity += targetPosition* shootSpeed * Time.deltaTime;
     yield return new WaitForSeconds(0.5f);
    }

    

}
