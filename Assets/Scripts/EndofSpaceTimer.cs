using System.Collections;
using UnityEngine;

public class EndofSpaceTimer : MonoBehaviour
{
    
    [SerializeField]
    private int timerInSeconds;

    private int minutes;
    private int seconds;

    private int secondsLeft;

    private void Start()
    {
        secondsLeft = timerInSeconds;
        StartCoroutine(CountDown(timerInSeconds));
    }


    IEnumerator CountDown(int timer)
    {
        for(int i = timer; i > 0; i--)
        {
            secondsLeft--;

            minutes = Mathf.FloorToInt(secondsLeft / 60);
            seconds = Mathf.FloorToInt(secondsLeft % 60);
            Debug.Log(minutes + ":" + seconds);
            yield return new WaitForSeconds(1);
            
        }
    }
}
