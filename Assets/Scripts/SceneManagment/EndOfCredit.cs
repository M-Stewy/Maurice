using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EndOfCredit : MonoBehaviour
{
    public float Seconds;
    [SerializeField] AudioClip speedRunNoise;
    [SerializeField] GameObject TextForSpeedRun;
    private GameObject speedTimer;
    void Start()
    {
        if(FindObjectOfType<EndofSpaceTimer>() != null)
        {
            speedTimer = FindObjectOfType<EndofSpaceTimer>().transform.gameObject;
            speedTimer.SetActive(false);
            TextForSpeedRun.SetActive(false);
            StartCoroutine(Wait(Seconds, true));
        }
        else
        {
            TextForSpeedRun.SetActive(false);
            StartCoroutine(Wait(Seconds, false));
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            SceneManager.LoadScene("TitleScreen");
        }
    }

    IEnumerator Wait(float time, bool speedRun)
    {
        if (speedRun)
        {
            yield return new WaitForSeconds(time);
            FindObjectOfType<Camera>().GetComponent<VideoPlayer>().enabled = false;
            TextForSpeedRun.SetActive(true);
            speedTimer.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(speedRunNoise);
            yield return new WaitForSeconds(7.5f);
            Destroy(speedTimer);
        }
        else
            yield return new WaitForSeconds(time);

        SceneManager.LoadScene("TitleScreen");
    }
}
