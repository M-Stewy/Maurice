using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
// Made by Jeb
public class ItemBob : MonoBehaviour
{ 
    public float transformAmount=.015f;
    public float Time=.05f;
    public int Iterations=30;
    public int WaitTimeDivisor = 6;
    private int counter=0;
    private bool stop = false;
    private bool isNeg = false;
    void FixedUpdate()
    {
        if (stop) {return; }
        if (isNeg == true)
        {
            this.transform.position = this.transform.position - new Vector3(0f, -transformAmount, 0f);
            StartCoroutine(wait(Time*(counter/WaitTimeDivisor)));
            counter++;
            if (counter == Iterations) { isNeg = false; counter = 0; StartCoroutine(wait(Time)); }
        } 
        else
        {
            this.transform.position = this.transform.position - new Vector3(0f, transformAmount, 0f);
            StartCoroutine(wait(Time*(counter/WaitTimeDivisor)));
            counter++;
            if (counter == Iterations) {  isNeg = true; counter = 0; StartCoroutine(wait(Time)); }
        }

    }

    IEnumerator wait(float num)
    {
        //Debug.Log("Reached");
        stop = true;
        yield return new WaitForSeconds(num);
        stop = false;
    }
}
    
