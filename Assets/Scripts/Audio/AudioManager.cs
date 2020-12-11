using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    public static AudioManager instance;

    

    private float randomPitch;


    // Start is called before the first frame update
    void Awake()
    {
       
       if ( instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
        
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch /*+ randomPitch*/;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.audioMixer.outputAudioMixerGroup;
            /*
              s.source.spatialBlend = s.spatialBlend;
              s.source.rolloffMode = s.rolloffMode;
              s.source.minDistance = ;
              s.source.maxDistance = ;
            */
            s.source.dopplerLevel = 0.5f;
            s.source.spread = 120f;
        }
    }

   public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }

        if (PauseManager.gameispaused)
        {
            s.source.volume *= .5f;
        }

        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }

        s.source.Stop();
    }
    void Update()
    {
       // randomPitch = UnityEngine.Random.Range(sound.minPitch, sound.maxPitch);
    }
}
