using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

//Made by Jeb

public class SceneChange : MonoBehaviour
{
    private bool CanAcessesKyleScene = false;


    public float xpos = 0;
    public float ypos = 0;
    public float standardypos = -3.925f;
    public GameObject Capsule;
    public GameObject positionTracker;
    public GameObject JebEnd;

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
        if (Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.Y) && Input.GetKey(KeyCode.L)) CanAcessesKyleScene = true;
        


        //Debug.Log("Triggered");
        if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Joystick1Button3))
        {
            if (SceneManager.GetActiveScene().name == "hubWorld")
            {
                xpos = Capsule.transform.position.x;
                ypos = Capsule.transform.position.y;
                positionTracker.GetComponent<positionTracker>().updatePosition();

            }

            if (other.name == "hubWorld")
            {
                if (SceneManager.GetActiveScene().name.ToString() == "KyleScene")
                {
                    GameObject.FindWithTag("Respawn").GetComponent<positionTracker>().KyleDone = true;
                }
                else if (SceneManager.GetActiveScene().name.ToString() == "StewyScene")
                {
                    GameObject.FindWithTag("Respawn").GetComponent<positionTracker>().StewyDone = true;
                }
                else if (SceneManager.GetActiveScene().name.ToString() == "NikoScene")
                {
                    GameObject.FindWithTag("Respawn").GetComponent<positionTracker>().NikoDone = true;
                }
                else if (SceneManager.GetActiveScene().name.ToString() == "JebScene")
                {
                    GameObject.FindWithTag("Respawn").GetComponent<positionTracker>().JebDone = true;
                    StartCoroutine(JebEndWait());
                }
                    
                GameObject.FindWithTag("Win").GetComponent<TitleScreenDisplay>().CallTitleDisplay();
                StartCoroutine(wait(4));
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
                if(CanAcessesKyleScene)
                    SceneManager.LoadScene("KyleScene");
                else
                    GameObject.FindGameObjectWithTag("Error Noise Thing").GetComponent<AudioSource>().Play();
            }
            else if (other.name == "JebScene")
            {
                SceneManager.LoadScene("JebScene");
            }
            else if (other.name == "SuperSecertScottShowdown")
            {
                if (/*positionTracker.GetComponent<positionTracker>().KyleDone == true &&*/ positionTracker.GetComponent<positionTracker>().NikoDone == true && positionTracker.GetComponent<positionTracker>().StewyDone == true && positionTracker.GetComponent<positionTracker>().JebDone == true)
                {
                   // SceneManager.LoadScene("SuperSecertScottShowdown"); //UnComment This Later
                }else
                {
                    GameObject.FindGameObjectWithTag("Error Noise Thing").GetComponent<AudioSource>().Play(); // I just thought this would be fun :)
                }
                
            }
        }

    }

    IEnumerator wait(float num)
    {
        yield return new WaitForSeconds(num);
        SceneManager.LoadScene("hubWorld");
    }

    IEnumerator JebEndWait() {
        //GameObject.FindWithTag("DisableAtEnd").SetActive(false);
        JebEnd.SetActive(true);
        yield return new WaitForSeconds(3f);
        JebEnd.SetActive(false);
        GameObject.FindWithTag("MainCamera").GetComponent<CamFollowPlayer>().PlayerTrans = JebEnd.transform;
    }

}
