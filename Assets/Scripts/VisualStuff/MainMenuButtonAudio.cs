using UnityEngine;
/// <summary>
/// Made by Stewy
/// 
/// plays noise for buttons
/// </summary>
public class MainMenuButtonAudio : MonoBehaviour
{
    AudioSource aS;
    [SerializeField] AudioClip buttonSelected;
    [SerializeField] AudioClip buttonPressed;

    private void Awake()
    {
        aS = GetComponent<AudioSource>();
        aS.ignoreListenerPause = true;
    }

    public void playSelectSFX()
    {
        aS.PlayOneShot(buttonSelected);
    }
    public void playClickSFX()
    {
        aS.PlayOneShot(buttonPressed);
    }
 
}
