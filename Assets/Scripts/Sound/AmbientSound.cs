using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author: Tomas
/// Provides Background Sounds that do not conistently play.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AmbientSound : MonoBehaviour
{
    [Tooltip("Audio File To Play")]
    public AudioClip audioClip;

    public float frequency;

    public bool playOnStart;
    private AudioSource source;
    private float timeRemaining;
    private float interval;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    void Start()
    {
        interval = audioClip.length + frequency;
        timeRemaining = interval;
        source.clip = audioClip;
        if(playOnStart)
        {
            source.Play();
        }
    }



    // Update is called once per frame
    void Update()
    {
        print(timeRemaining);
        timeRemaining -= Time.deltaTime;
        if(timeRemaining < 0)
        {
            timeRemaining = interval;
            source.Play();
        }
    }
}
