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

    [SerializeField]
    private bool UseAudio;

    [SerializeField]
    private bool shouldMuteMusic;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        if (OnStart)
        {
            if (UseAudio)
            {
                GetComponent<AudioSource>().loop = false;
                GetComponent<AudioSource>().Play();
            }
            StartCoroutine(TitleDisplay(titleTime));

            if(shouldMuteMusic)
            {
                if (FindObjectOfType<MusicManager>())
                {
                    FindObjectOfType<MusicManager>().PauseMusicforSec(titleTime - 0.5f);
                }
                if(FindObjectOfType<soundToggle>()) 
                {
                    FindObjectOfType<soundToggle>().MuteMusic();
                }
            }
        }
           
    }

    private IEnumerator TitleDisplay(float time)
    {
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds (time);
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void CallTitleDisplay()
    {
        if (UseAudio)
        {
            GetComponent<AudioSource>().loop = false;
            GetComponent<AudioSource>().Play();
        }
        if (shouldMuteMusic)
        {
            if (FindObjectOfType<MusicManager>())
            {
                FindObjectOfType<MusicManager>().PauseMusicforSec(titleTime - 0.5f);
            }
        }

        Debug.Log("called title display");
        StartCoroutine(TitleDisplay(titleTime));
    }
}
