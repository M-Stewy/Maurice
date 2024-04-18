using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

//Made by Jeb

public class SceneChange : MonoBehaviour
{

    public float xpos;
    public float ypos;
    public float standardypos = -3.925f;
    public GameObject Capsule;
    public GameObject positionTracker;
    public bool KyleDone = false, StewyDone = false, NikoDone = false, JebDone = false;

    private void Awake()
    {
        Capsule = GameObject.Find("Player(2.0)");
        positionTracker = GameObject.Find("positionTracker");

        if (SceneManager.GetActiveScene().name == "hubWorld")
        {
            transform.position = new Vector2(positionTracker.GetComponent<positionTracker>().xpos, -3.925f/*positionTracker.GetComponent<positionTracker>().ypos*/);
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("Triggered");
        if (Input.GetKey(KeyCode.E))
        {
            if (SceneManager.GetActiveScene().name == "hubWorld")
            {
                xpos = Capsule.transform.position.x;
                ypos = Capsule.transform.position.y;
                positionTracker.GetComponent<positionTracker>().updatePosition();

            }

            if (other.name == "hubWorld")
            {
                GameObject.FindWithTag("Win").GetComponent<TitleScreenDisplay>().CallTitleDisplay();
                StartCoroutine(wait(5));
                //SceneManager.LoadScene("hubWorld");
            }
            else if (other.name == "NikoScene")
            {
                SceneManager.LoadScene("NikoScene");
            }
            else if (other.name == "StewyScene")
            {
                SceneManager.LoadScene("StewyScene");
            }
            else if (other.name == "KyleScene")
            {
                SceneManager.LoadScene("KyleScene");
            }
            else if (other.name == "JebScene")
            {
                SceneManager.LoadScene("JebScene");
            }
            else if (other.name == "SuperSecretScottShowdown")
            {
                if (KyleDone == true && NikoDone == true && StewyDone == true && JebDone == true)
                {
                    SceneManager.LoadScene("SuperSecretScottShowdown");
                }
                
            }
        }

    }

    IEnumerator wait(float num)
    {
        if (SceneManager.GetActiveScene().name.ToString() == "KyleScene")
        {
            KyleDone = true;
            GameObject.FindWithTag("Respawn").GetComponent<positionTracker>().KyleDone = true;
        }
        else if (SceneManager.GetActiveScene().name.ToString() == "StewyScene")
        {
            StewyDone = true;
            GameObject.FindWithTag("Respawn").GetComponent<positionTracker>().StewyDone = true;
        }
        else if (SceneManager.GetActiveScene().name.ToString() == "NikoScene")
        {
            NikoDone = true;
            GameObject.FindWithTag("Respawn").GetComponent<positionTracker>().NikoDone = true;
        }
        else if (SceneManager.GetActiveScene().name.ToString() == "JebScene")
        {
            JebDone = true;
            GameObject.FindWithTag("Respawn").GetComponent<positionTracker>().JebDone = true;
        }
        yield return new WaitForSeconds(num);
        SceneManager.LoadScene("hubWorld");
    }

}
