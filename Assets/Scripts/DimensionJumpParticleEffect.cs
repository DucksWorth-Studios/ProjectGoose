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

    [Tooltip("The rate at which the alpha changes in the particle effect")]
    private float alphaChangeRate = 0.15f;

    [Tooltip("The amount of time the effect should play for")]
    private float effectPlayTime = 3f;

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
        
    }
}
