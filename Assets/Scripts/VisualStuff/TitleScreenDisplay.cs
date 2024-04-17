using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Made by Stewy
/// 
/// Displays a title screen of whatever
/// 
/// </summary>

public class TitleScreenDisplay : MonoBehaviour
{
    [SerializeField]
    private float titleTime;
    [SerializeField]
    private bool OnStart;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        if(OnStart)
            StartCoroutine(TitleDisplay(titleTime)  );
    }

    private IEnumerator TitleDisplay(float time)
    {
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds (time);
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void CallTitleDisplay()
    {
        Debug.Log("called title display");
        StartCoroutine(TitleDisplay(titleTime));
    }
}
