using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEngine : MonoBehaviour {

    //configuration parameters

    [SerializeField] float aimingRadious = 3f;
    [SerializeField] float aimingAreaYOffset = 1.75f;
    public static Vector2 mousePosition;
    Transform aimingArea;

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
}
