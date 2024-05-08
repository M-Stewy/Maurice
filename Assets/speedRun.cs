using UnityEngine;
using UnityEngine.SceneManagement;

public class speedRun : MonoBehaviour
{
    [SerializeField]
    EndofSpaceTimer SpeedRunTimerDisplay;
    [SerializeField]
    Vector3 CamOffSet;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if(FindObjectOfType<Camera>())
        {
            transform.GetChild(0).transform.position = FindObjectOfType<Camera>().transform.position + CamOffSet;
        }


        if(SceneManager.GetActiveScene().name == "hubWorld" || SceneManager.GetActiveScene().name == "TitleScreen" || SceneManager.GetActiveScene().name == "Credits")
        {
            SpeedRunTimerDisplay.ShouldBeCounting = false;
        }
        else
        {
            SpeedRunTimerDisplay.ShouldBeCounting = true;
        }
    }


}
