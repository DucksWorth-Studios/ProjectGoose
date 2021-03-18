using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author: Tomas
/// Provides Background Sounds that do not conistently play.
/// </summary>
public class AmbientSound : MonoBehaviour
{
    [Tooltip("Audio File To Play")]
    public AudioClip audioClip;

    public float frequency = 0;

    private AudioSource source;
    private float timeRemaining;
    private float interval;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
