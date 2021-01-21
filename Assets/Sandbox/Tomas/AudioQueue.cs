using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioQueue : MonoBehaviour
{
    public AudioClip[] clips;
    private int currentIndex = 0;
    private AudioSource audioSource;
    private bool MustPlay = false;
    private bool IsPaused = false;
    private int IndexMax = 0;
    void Start()
    {
        IndexMax = clips.Length;
    }
    public void Play(AudioSource audio)
    {
        audioSource = audio;
        MustPlay = true;
    }

    public void Pause()
    {
        IsPaused = true;
    }

    public void UnPause()
    {
        IsPaused = false;
    }

    public void OnParticleSystemStopped()
    {
        MustPlay = false;
        audioSource.Stop();
    }
    // Update is called once per frame
    void Update()
    {
        if(MustPlay && !audioSource.isPlaying && !IsPaused)
        {
            if(currentIndex < IndexMax)
            {
                audioSource.clip = clips[currentIndex];
                audioSource.Play();
                currentIndex++;
            }
            else
            {
                MustPlay = false;
            }
            
        }
    }
}
