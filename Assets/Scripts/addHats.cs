using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

//Made by Jeb

public class addHats : MonoBehaviour
{
    public GameObject[] chosen = new GameObject[9];
    private int randomNum;
    public UnityEvent increasePos;
    public UnityEvent decreasePos;
    public int childNum;
    private GameObject childForMove;
    public float distance;

    public void Awake()
    {
        for (int i=0; i<9; i++)
        {
            chosen[i].transform.position = new Vector3(0f, 0f, 0f);
        }
        
    }

    public void recieveHat()
    {
        randomNum = Random.Range(0, 9);
        childNum = this.transform.childCount;
        Instantiate(chosen[randomNum], this.transform);
        if (childNum != 0)
        {
            childForMove = this.transform.GetChild(childNum-1).gameObject;
            distance = childForMove.GetComponent<HatPos>().distance;
            this.transform.position = this.transform.position + new Vector3(0f, distance, 1f);
            increasePos.Invoke();
        } else
        {
            distance = GameObject.FindWithTag("Hat").GetComponent<HatPos>().distance;
            increasePos.Invoke();
        }
        
    }

    public void destroyHat()
    {
        childNum = this.transform.childCount;
        Destroy(this.transform.GetChild(childNum-1).gameObject);
        decreasePos.Invoke();
        childForMove = this.transform.GetChild(childNum - 1).gameObject;
        distance = childForMove.GetComponent<HatPos>().distance;
        if (childNum-1 != 0)
        {
            this.transform.position = this.transform.position - new Vector3(0f, distance, 1f);
        }
        
    }
}
