using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public static UI instance;
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

    /*private void Update()
    {
        currentScene = SceneManager.GetActiveScene().name;
        if (SceneManager.GetActiveScene().name != currentScene) 
        { 
            
        }
    }*/
}
