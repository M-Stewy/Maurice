using System.Collections;
using UnityEngine;
/// <summary>
/// Made by Stewy
/// 
/// starts the end of level timer for my level
/// </summary>
public class EndofSpaceTimer : MonoBehaviour
{
    [SerializeField]
    Vector3 PositionOffset;

    [SerializeField]
    SpriteRenderer Mins;
    [SerializeField]
    SpriteRenderer Sec10s;
    [SerializeField]
    SpriteRenderer Sec1s;
    [SerializeField]
    private int timerInSeconds;

    [SerializeField]
    Sprite[] Numbers;

    private int minutes;
    private int seconds;

    private int seconds10s;
    private int seconds1s;

    private int secondsLeft;

    [SerializeField]  bool countingUp;
    private Vector3 velocity = Vector3.zero;
    private void Start()
    {
        if (!countingUp) {
            secondsLeft = timerInSeconds;
            StartCoroutine(CountDown(timerInSeconds));
        } else {
            secondsLeft = 0;
            StartCoroutine(CountUp(0));
        }
    }

    private void Update()
    {
        SetSprites();

        transform.position = Vector3.SmoothDamp(transform.position, FindObjectOfType<Camera>().transform.position - PositionOffset, ref velocity,0.001f);
    }
    IEnumerator CountDown(int timer)
    {
        for(int i = timer; i > 0; i--)
        {
            secondsLeft--;

            minutes = Mathf.FloorToInt(secondsLeft / 60);
            seconds = Mathf.FloorToInt(secondsLeft % 60);

            seconds10s = Mathf.FloorToInt(seconds / 10);
            seconds1s = Mathf.FloorToInt(seconds % 10);
            //Debug.Log(minutes + ":" + seconds);
            yield return new WaitForSeconds(1);
            
        }
    }
    IEnumerator CountUp(int timer)
    {
        while(countingUp)
        {
            secondsLeft++;

            minutes = Mathf.FloorToInt(secondsLeft / 60);
            seconds = Mathf.FloorToInt(secondsLeft % 60);

            seconds10s = Mathf.FloorToInt(seconds / 10);
            seconds1s = Mathf.FloorToInt(seconds % 10);
            //Debug.Log(minutes + ":" + seconds);
            yield return new WaitForSeconds(1);

        }
    }

    private void SetSprites()
    {
        Mins.sprite = Numbers[minutes];
        Sec10s.sprite = Numbers[seconds10s];
        Sec1s.sprite = Numbers[seconds1s];
    }

}
