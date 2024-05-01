using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] bool MultiPlay;
    public float Volume;
    [Space]
    [SerializeField] private float fadeInTime;
    [SerializeField] private float fadeOutTime;
    [SerializeField] private float increant;
    [Space]
    
    AudioSource[] MusicSource;

    public List<AudioSource> CurrentMusicSources = new List<AudioSource>();

    bool fadingIn;
    bool fadingOut;

    void Start()
    {
        fadingIn = true;
        MusicSource = GetComponentsInChildren<AudioSource>();
        foreach (AudioSource source in MusicSource)
        {
            source.volume = Volume;
            source.mute = true;
        }


        if (MultiPlay) {
            for (int i = CurrentMusicSources.Count - 1; i >= 0; i--)
            {
                CurrentMusicSources[i].mute = false;
                FadeInTrack(i);
            }
        } 
        else
        {
            CurrentMusicSources.AddRange(MusicSource);
            foreach (AudioSource source in MusicSource)
            {
                source.mute = true;
            }
            MusicSource[0].mute = false;
            FadeInTrack(0);
        }
    }

    private void Update()
    {
        foreach(AudioSource source in MusicSource)
        {
            if(!fadingIn)
            {
                //Debug.Log("test for fade");
                source.volume = Volume;
            }
        }
    }

    public void ChangeCurrentTrack(int track)
    {
        if (MultiPlay)
        {
            CurrentMusicSources[track].mute ^= true;
        }
        else 
        {
            for (int i = MusicSource.Length - 1; i >= 0; i--)
            {
                if (i != track)
                {
                    MusicSource[i].mute = true;
                }
            }
        }
        
    }

    void FadeInTrack(int track)
    {
        StartCoroutine(FadeIn(track, fadeInTime, increant) );
    }

    IEnumerator FadeIn(int track, float sec, float incr)
    {
        fadingIn = true;
        CurrentMusicSources[track].volume = 0;
        CurrentMusicSources[track].mute = false;
        for (int i = 0; i < sec; i++) 
        {
            MusicSource[track].volume += incr;
            yield return new WaitForSeconds(1/sec);
            if (CurrentMusicSources[track].volume > Volume) break;
        }
        CurrentMusicSources[track].volume = Volume;
        fadingIn = false;
    }

    public void SetCurrentSources(int track)
    {
        
            if (!CheckForAlreadyThere(track) )
            {
                CurrentMusicSources.Add(MusicSource[track]);
                CurrentMusicSources[MusicToCurrConvert(track)].mute = false;

            } else if(CheckForAlreadyThere(track) )
            {
                CurrentMusicSources[MusicToCurrConvert(track)].mute = true;
                CurrentMusicSources.Remove(MusicSource[track]);
               
                
        }
        
    }

    private bool CheckForAlreadyThere(int i)
    {
        foreach (var source in CurrentMusicSources)
        {
          if(MusicSource[i] == source)
                return true;
        }
        return false;
    }

    int MusicToCurrConvert(int Mindex)
    {
        for(int i = CurrentMusicSources.Count - 1; i >= 0; i--)
        {
            if (CurrentMusicSources[i] == MusicSource[Mindex])
            {
                Debug.Log(i);
                return i;
            }
        }

        return -1;
    }


    public void StopAllMusic()
    {
        foreach(var source in MusicSource)
        {
            source.Stop();
        }
    }

    public void PauseMusicforSec(float sec)
    {
        StartCoroutine(pauseMusic(sec));
    }

    IEnumerator pauseMusic(float sec)
    {
        foreach(var S in CurrentMusicSources)
        {
            S.mute = true;
        }
        yield return new WaitForSeconds(sec);
        foreach (var S in CurrentMusicSources)
        {
            S.mute = false;
        }
    }
}
