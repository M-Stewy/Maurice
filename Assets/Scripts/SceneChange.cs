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
                SceneManager.LoadScene("hubWorld");
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
        }

    }

    //Also in positionTracker and Player scripts
    /*public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.FindWithTag("Respawn").GetComponent<positionTracker>().checkpoint();
        }
    }*/
}
