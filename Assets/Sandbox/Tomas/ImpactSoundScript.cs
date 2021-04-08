using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author: Tomas Modified Version of Script Made By ANdrew
/// Used to add impact sounds to world.
/// </summary>

[RequireComponent(typeof(AudioSource))]
public class ImpactSoundScript : MonoBehaviour
{

    [Tooltip("The magnitude of the velocity that is needed for the object to break")]
    public float breakingVelocity = 2;

    [Tooltip("Sound of object when it is dropped")]
    public AudioClip clip;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
    }
    /// <summary>
    /// Called when this object collides with another object. If the collision force is large enough the object will spawn the broken object and destroy this one.
    /// </summary>
    /// <param name="collision">The object this object is colliding with</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude < breakingVelocity)
            return;


        if (clip != null)
            audioSource.PlayOneShot(clip);
        else
            Debug.LogError("No Audio Clip Provided to Impact Script");

    }
}
