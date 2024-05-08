using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfCredit : MonoBehaviour
{
    public float Seconds;
    void Start()
    {
        StartCoroutine(Wait(Seconds));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            SceneManager.LoadScene("TitleScreen");
        }
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("TitleScreen");
    }
}
