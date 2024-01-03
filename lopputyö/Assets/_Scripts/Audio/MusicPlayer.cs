using UnityEngine.Audio;
using System;
using UnityEngine;

public class MusicPlayer : Singleton<MusicPlayer>
{
    const int AMOUNT = 6;
    AudioSource[] sources = new AudioSource[AMOUNT];


    void Start()
    {
        //Init();
    }

    void Init()
    {

        if (!AudioManager.Instance.IsPlaying("M1"))
        {
            for (int i = 0; i < AMOUNT; i++)
            {
                sources[i] = AudioManager.Instance.PlayOnLoop("M" + (i+1).ToString());
            }
        }
     
    }


    public void SetSourcesActiveUntil(int _stop)
    {
        for (int i = 0; i < _stop; i++)
        {
            sources[i].volume = 1;
        }
        for (int i = _stop; i < AMOUNT; i++)
        {
            sources[i].volume = 0;
        }
    }

    public void SetSourceActive(int _i)
    {
        sources[_i].volume = 1;
    }

    public void SilenceSource(int _i)
    {
        sources[_i].volume = 0;
    }

}