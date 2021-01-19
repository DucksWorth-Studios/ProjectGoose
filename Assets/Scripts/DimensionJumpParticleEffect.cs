using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

/// <summary>
/// Component that will control the particle effect around the player when they jump dimension
/// Author: Andrew Carolan
/// </summary>
public class DimensionJumpParticleEffect : MonoBehaviour
{
    private VisualEffect particleEffect;

    [Tooltip("The amount of time the effect should play for")]
    private float effectPlayTime = 2f;

    private bool isParticleActive = false;

    private float startTime = -1;

    // Start is called before the first frame update
    void Start()
    {
        particleEffect = GetComponent<VisualEffect>();

        if(particleEffect != null)
            particleEffect.Stop();

        EventManager.instance.OnTimeJump += StartParticles;
    }

    // Update is called once per frame
    void Update()
    {
        if(isParticleActive)
        {
            if (startTime == -1)
            {
                startTime = Time.time;
                particleEffect.SendEvent("OnStartPlay");
            }
            else if(startTime + effectPlayTime <= Time.time)
            {
                particleEffect.SendEvent("OnStopParticle");
                isParticleActive = false;
                startTime = -1;
            }
        }
    }

    /// <summary>
    /// Sets a bool to allow the code to change the opactiy of the particle effect.
    /// </summary>
    private void StartParticles()
        => isParticleActive = true;
}
