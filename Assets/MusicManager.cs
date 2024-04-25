using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    AudioSource[] MusicSource;

    void Start()
    {
        foreach (AudioSource source in MusicSource)
        {
            source.Play();
            source.mute = true;
        }
        MusicSource[0].mute = false;
    }

    public void ChangeCurrentTrack(int track)
    {
        for(int i  = MusicSource.Length-1; i >= 0; i--)
        {
            if(i != track)
            {
                MusicSource[i].mute = true;
            }
        }
    }


}
