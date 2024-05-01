using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
/// <summary>
/// Made by Stewy and Jeb
/// 
/// Stewy did basic pause/unpause and exit/quit functionaity
/// Jeb did tutorial functionality
/// </summary>
public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject firstButton;

    [SerializeField] GameObject backFirstButton;
    public GameObject TheTutorial;
    public GameObject Xbox;
    public GameObject PS;
    public GameObject KBM;

    AudioSource aS;
    [SerializeField] AudioClip hitPause;
    [SerializeField] AudioClip hitUnpause;
    [SerializeField] AudioClip buttonSelected;
    [SerializeField] AudioClip buttonPressed;

    private void Awake()
    {
        aS = GetComponent<AudioSource>();
        aS.ignoreListenerPause = true;
    }
    private void Update()
    {

        // the PS controller can pause by default (not entirely sure why) but I dont know about the Xbox.
            
        if (FindObjectOfType<PlayerInputHandler>().isXbox)
        {
            //Debug.Log("PAUSE: XboxGet");
            if (Input.GetKeyDown(KeyCode.Joystick1Button7) /*Put controller input here aswell*/)
                        {
                            if (!isPaused)
                                PauseGame();
                            else
                                ResumeGame();
                        }
        }
            

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button9))
        {
            if (!isPaused)
             PauseGame();
            else
             ResumeGame();
        }

    }
   

    public void PauseGame()
    {
        aS.volume = 1.0f;
        aS.PlayOneShot(hitPause);
        Time.timeScale = 0f;
        Menu.SetActive(true);
        AudioListener.pause = true;
        isPaused = true;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    public void ResumeGame()
    {
        aS.volume = 1.0f;
        aS.PlayOneShot(hitUnpause);
        Menu.SetActive(false);
        Time.timeScale = 1.0f;
        AudioListener.pause = false;
        isPaused = false;
    }

    public void BackToHub()
    {
        ResumeGame();
        SceneManager.LoadScene("hubWorld");
    }

    public void Quit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void Tutorial()
    {
        Menu.SetActive(false);
        TheTutorial.SetActive(true);
        if (FindObjectOfType<PlayerInputHandler>().isXbox)
        {
            Xbox.SetActive(true);
        }
        else if (FindObjectOfType<PlayerInputHandler>().isPS)
        {
            PS.SetActive(true);
        }
        else
        {
            KBM.SetActive(true);
        }
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(backFirstButton);
    }

    public void TutorialBack()
    {
        //Debug.Log("Reached");
        TheTutorial.SetActive(false);
        Xbox.SetActive(false);
        PS.SetActive(false);
        KBM.SetActive(false);
        Menu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);

    }



    public void PressedSFX()
    {
        aS.volume = 1.0f;
        aS.PlayOneShot(buttonPressed);
    }

    public void PlaySFXOnSelect()
    {
        aS.volume = 0.5f;
        aS.PlayOneShot(buttonSelected);
        Debug.Log("slected a button");
    }
}
