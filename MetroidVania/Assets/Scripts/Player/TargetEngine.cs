using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEngine : MonoBehaviour {

    //configuration parameters

    [SerializeField] float aimingRadious = 3f;
    [SerializeField] float aimingAreaYOffset = 1.75f;
    public static Vector2 mousePosition;
    public float zAngle;
    Transform aimingArea;
    [SerializeField] float offset;

    // Use this for initialization
    void Start()
    {
        
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GetTargetPosition();
    }

    public Vector2 GetTargetPosition()
    {
        aimingArea = GameObject.Find("Player").transform;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimingAreaCenter = aimingArea.localPosition;
        aimingAreaCenter += new Vector2(0, aimingAreaYOffset);
        Vector2 v = mousePosition - aimingAreaCenter;
        v = Vector2.ClampMagnitude(v, aimingRadious);
        mousePosition = aimingAreaCenter + v;
        return mousePosition;
    }

    //Code obtained from https://www.youtube.com/watch?v=bY4Hr2x05p8&t=29s
    //Use this code to obtain the angle the projectile is been launched, can be used to change the animations later
    public void getAimingAngle() {
        Vector2 differenceBetweenOriginAndTarget = 
            mousePosition - new Vector2(transform.position.x, transform.position.y);
        zAngle = Mathf.Atan2(differenceBetweenOriginAndTarget.y, differenceBetweenOriginAndTarget.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, 0f, zAngle + offset);
    }
}
