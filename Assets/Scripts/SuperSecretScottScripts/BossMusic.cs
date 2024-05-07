using UnityEngine;

public class BossMusic : MonoBehaviour
{
    [SerializeField] AudioSource musicMan;
    [SerializeField] AudioClip Prelude;
    [SerializeField] AudioClip Intro;
    [SerializeField] AudioClip Track1;
    [SerializeField] AudioClip Track2;
    [SerializeField] AudioClip Track3;
    [SerializeField] AudioClip Outro;

    [HideInInspector]
    public int currentTrack; // should only ever be between 0 and 5
    [HideInInspector]
    public bool fightCanStart;
    private bool canStop;

    private void Update()
    {
        if(!musicMan.isPlaying)
        {
            switch (currentTrack)
            {
                case 0:
                    musicMan.PlayOneShot(Prelude);
                    fightCanStart = false;
                    break;
                case 1:
                    musicMan.PlayOneShot(Intro);
                    currentTrack = 2;
                    break;
                case 2:
                    musicMan.PlayOneShot(Track1);
                    fightCanStart = true;
                    break;
                case 3:
                    musicMan.PlayOneShot(Track2);
                    break;
                case 4:
                    musicMan.PlayOneShot(Track3);
                    break;
                case 5:
                    musicMan.PlayOneShot(Outro);
                    canStop = true;
                    break;
                default:
                    break;
            }
                 
            if(canStop)
            {
                musicMan.mute = true;
            }
            
        }
    }

    public void StopAllMusic()
    {
        musicMan.mute = true;
    }


}
