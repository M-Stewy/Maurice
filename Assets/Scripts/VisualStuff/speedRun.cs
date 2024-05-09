using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Made by Stewy
/// 
/// logic for the speedRun timer to know when to be active or not
/// </summary>
public class speedRun : MonoBehaviour
{
    [SerializeField]
    EndofSpaceTimer SpeedRunTimerDisplay;
    [SerializeField]
    Vector3 CamOffSet;
    private Vector3 TrueOffset;

    public speedRun instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
            
    }

    private void Update()
    {
        if(FindObjectOfType<Camera>())
        {
            transform.GetChild(0).transform.position = FindObjectOfType<Camera>().transform.position + TrueOffset;
        }


        if(SceneManager.GetActiveScene().name == "hubWorld" || SceneManager.GetActiveScene().name == "TitleScreen" || SceneManager.GetActiveScene().name == "Credits")
        {
            SpeedRunTimerDisplay.ShouldBeCounting = false;
        }
        else
        {
            SpeedRunTimerDisplay.ShouldBeCounting = true;
        }

        if(SceneManager.GetActiveScene().name == "Credits")
        {
            TrueOffset = new Vector3(5, 0, 10);
        }
        else
        {
            TrueOffset = CamOffSet;
        }


    }


}
