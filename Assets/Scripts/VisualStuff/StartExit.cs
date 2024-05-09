using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

//Made by Niko

public class StartExit : MonoBehaviour
{

    public GameObject SpeedRunTimer;
    public void ExitGame()
    {
        //This is for the app running in the unity editor
     //   UnityEditor.EditorApplication.isPlaying = false;


        //This one is for running as an exe outside of the unity editor
        Application.Quit();

    }

    public void SpeedRun()
    {
        SpeedRunTimer.SetActive(true);
        SpeedRunTimer.GetComponentInParent<EndofSpaceTimer>().SetSecsToZero();
        SceneManager.LoadScene("hubWorld");
    }

    public void Restart()
    {
        
    }

    public void StartGame()
    {
        Destroy(SpeedRunTimer.GetComponentInParent<EndofSpaceTimer>().gameObject);
        SceneManager.LoadScene("hubWorld");
    }
}
