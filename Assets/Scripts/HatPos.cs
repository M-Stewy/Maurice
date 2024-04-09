using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Made By Jeb

public class HatPos : MonoBehaviour
{
    public float distance = .45f;
    private float realDistance;
    public void changePos()
    {
        realDistance = GameObject.FindWithTag("HatStart").GetComponent<addHats>().distance;
        this.transform.position = this.transform.position - new Vector3(0f, realDistance, 0f);
    }

    public void decreasePos()
    {
        //Debug.Log("Reached");
        realDistance = GameObject.FindWithTag("HatStart").GetComponent<addHats>().distance;
        this.transform.position = this.transform.position + new Vector3(0f, realDistance, 0f);
    }
}
