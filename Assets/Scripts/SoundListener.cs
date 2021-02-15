using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Tomas
/// Will be attached to Enviromental objects To Listen for events to play.
/// </summary>
public class SoundListener : MonoBehaviour
{
    [Tooltip("Audio File To Play")]
    public AudioClip audioClip;
    [Tooltip("What Type Of Sound To Listen For")]
    public Sound soundToListen;
    [Tooltip("Should The Sound Loop")]
    public bool loop;

    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.loop = loop;
        EventManager.instance.OnPlaySound += PlayElement;
        EventManager.instance.OnStopSound += StopElement;
    }
    // Sound will be played when correct enum is send with the OnPlaySound Event
    private void PlayElement(Sound soundToPlay)
    {
        if(soundToPlay == soundToListen)
        {
            audioSource.Play();
        }
    }

    private void StopElement(Sound soundToPlay)
    {
        if (soundToPlay == soundToListen)
        {
            audioSource.Stop();
        }
    }
}
