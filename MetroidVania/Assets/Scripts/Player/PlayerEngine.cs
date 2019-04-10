using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEngine : MonoBehaviour {


    [Header("Player Attributes")]
    [SerializeField] float movementSpeed = 10f;

    private float deltaX;
    private float deltaY;
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Use this for initialization
    void Start () {
        setUpMoveBoundaries();
    }
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    private void Move()
    {
        float deltaMovement = Time.deltaTime * movementSpeed;
        deltaX = Input.GetAxis("Horizontal") * deltaMovement;
        var newPositionY = Mathf.Clamp(transform.position.y, xMin, xMax);
        var newPositionX = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        transform.position = new Vector2(newPositionX, newPositionY);
    }


    private void setUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0.05f, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(0.95f, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0.02f, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).y;
    }

}
