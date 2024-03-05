using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Made by Jeb

public class positionTracker : MonoBehaviour
{
    public float xpos;
    public float ypos;
    public GameObject SceneChange;
    void Awake()
    {
        DontDestroyOnLoad(GameObject.Find("positionTracker"));
    }

    public void updatePosition()
    {
        SceneChange = GameObject.Find("Player(2.0)");
        xpos=SceneChange.GetComponent<SceneChange>().xpos;
        ypos=SceneChange.GetComponent<SceneChange>().ypos;
    }
}
