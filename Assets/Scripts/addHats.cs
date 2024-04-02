using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

//Made by Jeb

public class addHats : MonoBehaviour
{
    public GameObject chosen;
    public float distance = .45f;
    public UnityEvent increasePos;
    public UnityEvent decreasePos;
    public GameObject reset;
    public int childNum;

    public void Awake()
    {
        reset.transform.position = new Vector3(0f, 0f, 0f);
    }

    public void recieveHat()
    {
        Instantiate(chosen, this.transform);
        increasePos.Invoke();
        this.transform.position = this.transform.position + new Vector3(0f, distance, 0f);
    }

    public void destroyHat()
    {
        childNum = this.transform.childCount;
        Destroy(this.transform.GetChild(childNum-1).gameObject);
        decreasePos.Invoke();
        this.transform.position = this.transform.position - new Vector3(0f, distance, 0f);
    }
}
