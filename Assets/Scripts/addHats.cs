using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Made by Jeb

public class addHats : MonoBehaviour
{
    public GameObject chosen;
    public float distance = .2f;
    public UnityEvent changePos;
    public GameObject reset;

    public void Awake()
    {
        reset.transform.position = new Vector3(0f, 0f, 0f);
    }

    public void recieveHat()
    {
        Instantiate(chosen, this.transform);
        changePos.Invoke();
        this.transform.position = this.transform.position + new Vector3(0f, distance, 0f);
    }
}
