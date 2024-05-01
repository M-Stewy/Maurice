using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [SerializeField]
    AudioClip Boom;

    private int minutes;
    private int seconds;

    private int seconds10s;
    private int seconds1s;

    private int secondsLeft;

    [SerializeField]  bool countingUp;
    [SerializeField] bool BOOOMonZero;
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

        if(secondsLeft <= 0 && BOOOMonZero)
        {
            AudioSource.PlayClipAtPoint(Boom, transform.position,Random.Range(.5f,1f));
            AudioSource.PlayClipAtPoint(Boom, transform.position + new Vector3(Random.Range(-10,10), Random.Range(-1, 1), 0), Random.Range(.5f, 1f));
            AudioSource.PlayClipAtPoint(Boom, transform.position + new Vector3(Random.Range(-1, 10), Random.Range(-5, 5), 0), Random.Range(.5f, 1f));
            AudioSource.PlayClipAtPoint(Boom, transform.position + new Vector3(Random.Range(-5, 10), Random.Range(-2, 20), 0), Random.Range(.5f, 1f));
            AudioSource.PlayClipAtPoint(Boom, transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-5, 5), 0), Random.Range(.5f, 1f));
            AudioSource.PlayClipAtPoint(Boom, transform.position + new Vector3(Random.Range(-1, 1), Random.Range(-1, 10), 0), Random.Range(.5f, 1f));
            AudioSource.PlayClipAtPoint(Boom, transform.position + new Vector3(Random.Range(-5, 5), Random.Range(-1, 10), 0), Random.Range(.5f, 1f));
            AudioSource.PlayClipAtPoint(Boom, transform.position + new Vector3(Random.Range(-2, 20), Random.Range(-1, 10), 0), Random.Range(.5f, 1f));
            AudioSource.PlayClipAtPoint(Boom, transform.position + new Vector3(Random.Range(-5, 5), Random.Range(-10, 10), 0), Random.Range(.5f, 1f));
            BOOOMonZero = false;
            GameObject.FindWithTag("Lose").GetComponent<TitleScreenDisplay>().CallTitleDisplay();
            if (FindObjectOfType<MusicManager>())
            {
                FindObjectOfType<MusicManager>().StopAllMusic();
            }
            StartCoroutine(wait(5));
        }
    }

    IEnumerator wait(float num)
    {
        yield return new WaitForSeconds(num);
        SceneManager.LoadScene("HubWorld");

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
