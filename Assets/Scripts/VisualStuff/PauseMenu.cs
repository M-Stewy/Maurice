using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject firstButton;

    private void Update()
    {

        // the PS controller can pause by default (not entirely sure why) but I dont know about the Xbox.
            
        if (FindObjectOfType<PlayerInputHandler>().isXbox)
        {
            Debug.Log("PAUSE: XboxGet");
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
        Time.timeScale = 0f;
        Menu.SetActive(true);
        AudioListener.pause = true;
        isPaused = true;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    public void ResumeGame()
    {
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
}
