using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TextMeshProUGUI UI;
    public string currentScene;
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
        GameObject.Find("Player(2.0)").GetComponent<Player>().playerData.health = 3;
        UI.text = GameObject.Find("Player(2.0)").GetComponent<Player>().playerData.health.ToString();
    }

    public void updatePosition()
    {
        SceneChange = GameObject.Find("Player(2.0)");
        xpos=SceneChange.GetComponent<SceneChange>().xpos;
        ypos=SceneChange.GetComponent<SceneChange>().ypos;
    }

    private void Update()
    {
        
        if (SceneManager.GetActiveScene().name != currentScene) 
        {
            currentScene = SceneManager.GetActiveScene().name;
            UI = GameObject.Find("Health").GetComponent<TextMeshProUGUI>();
            GameObject.Find("Player(2.0)").GetComponent<Player>().playerData.health = 3;
            UI.text = GameObject.Find("Player(2.0)").GetComponent<Player>().playerData.health.ToString();
        }
    }
}
