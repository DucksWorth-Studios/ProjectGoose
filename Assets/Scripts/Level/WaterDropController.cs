using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

/// <summary>
/// Author: Andrew Carolan 
/// Will control the water drop VFX and will play a sound when the droplet hits the ground
/// </summary>
[RequireComponent(typeof(VisualEffect))]
[RequireComponent(typeof(AudioSource))]
public class WaterDropController : MonoBehaviour
{
    [Tooltip("The water-droplet vfx")]
    VisualEffect vfx;

    [Tooltip("The sound to be played when the sound collides with the ground")]
    public AudioClip dropletSound;

    private float timeBetweenDrops = 2.5f;
    private float lastDropTime = 0;

    [Tooltip("The amount of time after the vfx has started to play the audio")]
    private float audioPlayTime = 0.5f;

    private bool canPlayAudio = false;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        vfx = GetComponent<VisualEffect>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = dropletSound;
    }

    // Update is called once per frame
    void Update()
    {
        if(lastDropTime + timeBetweenDrops < Time.time)
        {
            vfx.SendEvent("OnPlay");

            lastDropTime = Time.time;

            canPlayAudio = true;
        }

        if(lastDropTime + audioPlayTime <= Time.time && canPlayAudio)
        {
            canPlayAudio = false;

            if (dropletSound != null)
                audioSource.Play();
        }
    }
}
