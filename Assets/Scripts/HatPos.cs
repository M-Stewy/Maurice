using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Made By Jeb

public class HatPos : MonoBehaviour
{
    public float distance = .45f;
    public void changePos()
    {
        this.transform.position = this.transform.position - new Vector3(0f, distance, 0f);
    }

    public void decreasePos()
    {
        //Debug.Log("Reached");
        this.transform.position = this.transform.position + new Vector3(0f, distance, 0f);
    }
}
