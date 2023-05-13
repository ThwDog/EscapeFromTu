using System;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Audio : MonoBehaviour
{
    internal static Audio instance;
    public Sound[] sounds;

    private void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume; 
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }   
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            play("HorrorBg", true);
            Cursor.visible = true;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            play("Piano", true);
            Cursor.visible = false;
        }
    }


    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            play("Piano", false);
            Cursor.visible = true;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            play("HorrorBg", false);
            Cursor.visible = false;
        }
    }



    public void play(string name , bool play)
    {
        //Find Sound in sound array that is name file equal
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;

        if(play) s.source.Play();
        else s.source.Stop();
    }

    
}
