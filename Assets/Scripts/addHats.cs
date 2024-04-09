using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

//Made by Jeb

public class addHats : MonoBehaviour
{
    public GameObject[] chosen = new GameObject[4];
    private int randomNum;
    public UnityEvent increasePos;
    public UnityEvent decreasePos;
    public int childNum;
    private GameObject childForMove;

    public void Awake()
    {
        for (int i=0; i<4; i++)
        {
            chosen[i].transform.position = new Vector3(0f, 0f, 0f);
        }
        
    }

    public void recieveHat()
    {
        randomNum = Random.Range(0, 4);
        childNum = this.transform.childCount;
        Instantiate(chosen[randomNum], this.transform);
        increasePos.Invoke();
        if (childNum != 0)
        {
            childForMove = this.transform.GetChild(childNum-1).gameObject;
            this.transform.position = this.transform.position + new Vector3(0f, childForMove.GetComponent<HatPos>().distance, 0f);
        }
        
    }

    public void destroyHat()
    {
        childNum = this.transform.childCount;
        Destroy(this.transform.GetChild(childNum-1).gameObject);
        decreasePos.Invoke();
        childForMove = this.transform.GetChild(childNum - 1).gameObject;
        if (childNum-1 != 0)
        {
            this.transform.position = this.transform.position - new Vector3(0f, childForMove.GetComponent<HatPos>().distance, 0f);
        }
        
    }
}
