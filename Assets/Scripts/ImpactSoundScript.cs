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
    private bool isOver = false;
    private void Start()
    {
        EventManager.instance.OnProgress += DisableSound;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
    }

    private void DisableSound(STAGE stage)
    {
        if (stage == STAGE.END)
            isOver = true;
    }

    /// <summary>
    /// Called when this object collides with another object. If the collision force is large enough the object will spawn the broken object and destroy this one.
    /// </summary>
    /// <param name="collision">The object this object is colliding with</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude < breakingVelocity || isOver || collision.gameObject.tag == "RightHand" || collision.gameObject.tag == "Player")
            return;


        if (clip != null)
            audioSource.PlayOneShot(clip);
        else
            Debug.LogError("No Audio Clip Provided to Impact Script");

    }
}
