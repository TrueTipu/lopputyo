using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 0.5f;

    [Range(.1f, 3f)]
    public float pitch = 1f;

    [Range(-1f, 1f)]
    public float stereo = 0f;

    public AudioMixerGroup mixer;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

    public Sound(Sound s)
    {
        this.name = s.name;
        this.clip = s.clip;
        this.volume = s.volume;
        this.pitch = s.pitch;
        this.stereo = s.stereo;
        this.mixer = s.mixer;
        this.loop = s.loop;
    }
}
