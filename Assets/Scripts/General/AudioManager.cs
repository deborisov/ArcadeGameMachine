using System;
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public List<Sound> sounds;
    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume * PlayerPrefs.GetFloat("Volume", 0.5f);
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound sound = sounds.Find(s => s.name == name);
        sound?.source.Play();
    }

    public void Stop(string name)
    {
        Sound sound = sounds.Find(s => s.name == name);
        sound.source.Stop();
    }

    public void Start()
    {
        if (PlayerPrefs.GetInt("WasPrevious", 0) == 0)
        {
            Play("Theme");
        }
        else
        {
            PlayerPrefs.SetInt("WasPrevious", 0);
        }
    }
}
