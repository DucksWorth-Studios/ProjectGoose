using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author Tomas
/// This is used where a sound has two  potential outccomes
 /// </summary>
public class InverseSounds : MonoBehaviour
{
    public AudioClip clipOne;
    public AudioClip clipTwo;
    public Sound soundToPlay;
    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
        EventManager.instance.OnPlayOneSound += PlaySound;
        EventManager.instance.OnStopSound += StopSound;
    }

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

    private void StopSound(Sound sound)
    {
        if (sound == soundToPlay)
        {
            source.Stop();
        }
    }
}
