using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Made by Jeb
//Simple rotate around script, goes on main enemy object. Requires damage script on main object, and "center" object to be child of main.

public class rotateEnemy : MonoBehaviour
{
    public GameObject center;
    public float speed=40f;
    void Update()
    {
        transform.RotateAround(center.transform.position, new Vector3(0,0,10), speed*Time.deltaTime);
    }
}
