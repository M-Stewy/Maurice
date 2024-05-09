using UnityEngine;
/// <summary>
/// Made by Stewy
/// 
/// Simply randomizes explosions on spawn to give variation.
/// </summary>
public class eploContro : MonoBehaviour
{
    private void Awake()
    {
        var ass = GetComponent<AudioSource>();
        var anim = GetComponent<Animator>();


        ass.volume = Random.Range(0.2f, .65f);
        ass.pitch = Random.Range(.65f, 1.45f);
        anim.speed = Random.Range(0.5f, 2f);
    }
}
