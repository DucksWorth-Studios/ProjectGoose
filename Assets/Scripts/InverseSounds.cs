using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author Tomas
/// This is used where a sound has two  potential outccomes
 /// </summary>
public class InverseSounds : MonoBehaviour
{
    [Tooltip("Main Sound")]
    public AudioClip clipOne;
    [Tooltip("The Alternate Sound")]
    public AudioClip clipTwo;
    public Sound soundToPlay;
    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
        EventManager.instance.OnPlayOneSound += PlaySound;
        EventManager.instance.OnStopSound += StopSound;
    }
    /// <summary>
    /// Play Sound Takes in an enum to identify the sound and a bool.
    /// </summary>
    /// <param name="sound">Sound to Play</param>
    /// <param name="firstSound">Is it the first one if no play second sound</param>
    private void PlaySound(Sound sound,bool firstSound)
    {
        if(sound == soundToPlay)
        {
            if(firstSound && clipOne != null)
            {
                source.clip = clipOne;
                source.Play();
            }

            if(!firstSound && clipTwo != null)
            {
                source.clip = clipTwo;
                source.Play();
            }
        }
    }
    /// <summary>
    /// Stop Sound
    /// </summary>
    /// <param name="sound">Sound to play</param>
    private void StopSound(Sound sound)
    {
        if (sound == soundToPlay)
        {
            source.Stop();
        }
    }
}
