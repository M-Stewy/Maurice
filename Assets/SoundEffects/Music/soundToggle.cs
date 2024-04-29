using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundToggle : MonoBehaviour
{
    private AudioSource Audio;

    void Start()
    {
        Audio = this.GetComponent<AudioSource>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Joystick1Button3))) {
            Audio.mute = !Audio.mute;
        }
    }
}
