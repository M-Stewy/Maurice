using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Made by Jeb
public class soundToggle : MonoBehaviour
{
    private AudioSource Audio;

    void Start()
    {
        Audio = this.GetComponent<AudioSource>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Joystick1Button3))) {
            Audio.mute = !Audio.mute;
            Debug.Log("Reached");
        }
    }

    public void MuteMusic()
    {
        Audio.mute = true;
    }
}
