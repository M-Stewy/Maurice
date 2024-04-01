using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addHats : MonoBehaviour
{
    public GameObject chosen;
    public float distance = .5f;
    public Vector2 OBJ;

    public void recieveHat()
    {
        //(this.transform.position + new Vector3(0f, 0.75f, 0f))
        Instantiate(chosen, this.transform);
        this.transform.position = this.transform.position + new Vector3(0f, distance, 0f);
        GameObject.FindWithTag("Hat").transform.position = GameObject.FindWithTag("Hat").transform.position - new Vector3(0f, distance, 0f);
    }
}
