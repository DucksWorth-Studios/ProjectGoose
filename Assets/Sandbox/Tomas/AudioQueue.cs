using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author: Tomas
/// This will hold a scenes lines and whenc alled upon will play the lines in the correct order.
/// </summary>
public class AudioQueue : MonoBehaviour
{
    public AudioClip[] clips;
    private int currentIndex = 0;
    //Will Be Passed
    private AudioSource audioSource;
    //Has it been called to play
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

    public void Stop()
    {
        MustPlay = false;
        audioSource.Stop();
    }
    // Update is called once per frame
    void Update()
    {
        //If it has been called to play there is nothing currently being fired and its not paused
        //Itll play next clip
        if(MustPlay && !audioSource.isPlaying && !IsPaused)
        {
            //Ensures we dont go out of range
            if(currentIndex < IndexMax)
            {
                audioSource.clip = clips[currentIndex];
                audioSource.Play();
                currentIndex++;
            }
            else
            {
                //If List completed its job is done
                MustPlay = false;
            }
            
        }
    }
}
