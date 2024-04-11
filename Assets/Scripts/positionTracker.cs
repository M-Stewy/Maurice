using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

//Made by Jeb

public class positionTracker : MonoBehaviour
{
    public float xpos;
    public float ypos;
    public GameObject SceneChange;
    public static positionTracker instance;
    public string currentScene;
    public float checkpointX;
    public float checkpointY;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //THIS SETS HEALTH TO 3 WHEN SCENE STARTS-------------------------------------------------------------
        GameObject.FindWithTag("Player").GetComponent<Player>().playerData.health = GameObject.FindWithTag("Player").GetComponent<Player>().playerData.maxHealth;
    }

    public void updatePosition()
    {
        SceneChange = GameObject.FindWithTag("Player");
        xpos=SceneChange.GetComponent<SceneChange>().xpos;
        ypos=SceneChange.GetComponent<SceneChange>().ypos;
    }

    private void Update()
    {
        
        if (SceneManager.GetActiveScene().name != currentScene) 
        {
            currentScene = SceneManager.GetActiveScene().name;
            GameObject.FindWithTag("Player").GetComponent<Player>().playerData.health = GameObject.FindWithTag("Player").GetComponent<Player>().playerData.maxHealth;
        }
    }
}
