using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class SoundManager : MonoBehaviour
{

    public static SoundManager sM_Singleton;

    public Sound[] sounds;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (sM_Singleton != null)
        {
            Destroy(gameObject);
        }
        else
        {
            sM_Singleton = this;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.isLooping;
        }
    }

        // Start is called before the first frame update
        void Start()
    {
        PlaySound("Music");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sound()
    {
        if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
        }
        else if (AudioListener.volume == 1)
        {
            AudioListener.volume = 0;
        }
    }

    public void PlaySound(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        s.source.Play();

        if (s.source == null)
        {
            Debug.Log(soundName);
        }
    }
}
