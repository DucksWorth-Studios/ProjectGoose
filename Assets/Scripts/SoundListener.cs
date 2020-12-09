using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author tomas
/// The Enum will contain variables for all listening objects
/// </summary>
public enum Sound { DoorSound, Creaking, Breaking };
/// <summary>
/// Author: Tomas
/// Will be attached to Enviromental objects To Listen for events to play.
/// </summary>
public class SoundListener : MonoBehaviour
{

    AudioSource audioSource;
    [Tooltip("Audio File To Play")]
    public AudioClip audioClip;
    [Tooltip("What Type Of Sound To Listen For")]
    public Sound soundToListen;
    void Start()
    {
        audioSource.clip = audioClip;
        EventManager.instance.OnPlaySound += PlayElement;
    }
    // Sound will be played when correct enum is send with the OnPlaySound Event
    private void PlayElement(Sound soundToPlay)
    {
        if(soundToPlay == soundToListen)
        {
            audioSource.Play();
        }
    }
}
