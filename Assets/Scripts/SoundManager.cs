using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<AudioClip> soundEffects;
    public List<string> soundNames;
    public AudioSource audioSource;
    public static SoundManager instance;
    public AudioClip backgroundMusic;
    public void Awake()
    {
        //play background music on repeat always
        audioSource.loop = true;
        audioSource.clip = backgroundMusic;
        audioSource.volume = 0.5f;
        audioSource.Play();
        instance = this;
    }

    /// <summary>
    /// Play a sound effect. The sound effect is specified by name.
    /// </summary>
    /// <param name="name">The name of the sound effect to play.</param>
    public void PlaySound(string name)
    {
        audioSource.PlayOneShot(soundEffects[soundNames.IndexOf(name)]);
    }
}
