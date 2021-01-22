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

    // Start is called before the first frame update
    void Start()
    {
        vfx = GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        if(lastDropTime + timeBetweenDrops < Time.time)
        {
            PlayEffect();

            lastDropTime = Time.time;
        }
    }

    private IEnumerator PlayEffect()
    {
        vfx.SendEvent("OnPlay");

        yield return new WaitForSeconds(1.0f);

        if (dropletSound != null)
            AudioSource.PlayClipAtPoint(dropletSound, new Vector3(transform.position.x, 0, transform.position.z));
    }
}
