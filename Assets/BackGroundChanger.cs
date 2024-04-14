using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundChanger : MonoBehaviour
{
    [SerializeField]
    GameObject[] backGroundsToDisAble;

    [SerializeField]
    GameObject backGroundToEnable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach(GameObject go in backGroundsToDisAble) { go.SetActive(false); }
            backGroundToEnable.SetActive(true);
        }
    }

}
