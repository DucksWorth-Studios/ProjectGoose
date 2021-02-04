using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingController : MonoBehaviour
{
    [Tooltip("An array of lights that can flash at random points")]
    public Light[] lights;

    private bool canExecute = false;
    private bool isFlashing = false;

    private float startTime;

    private float nextPlayTime;

    private Color originalColor;
    private float originalIntensity;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.OnTimeJump += Activate;
    }

    // Update is called once per frame
    void Update()
    {
        if(canExecute)//Player is in the decayed dimension
        {
            if (isFlashing) return; // a light is already active
            else(startTime + nextPlayTime <= Time.time)
            {
                startTime = Time.time;
                isFlashing = true;
                StartCoroutine(FlashLight(lights[Random.Range(0, lights.Length)]));
            }
        }
    }

    private void Activate()
        => canExecute = true;

    private IEnumerator FlashLight(Light lightToFlash)
    {
        originalColor = lightToFlash.color;
        originalIntensity = lightToFlash.intensity;

        lightToFlash.color = Color.white;
        lightToFlash.intensity = 5;

        while(isFlashing)
        {
            if (Time.time - startTime > 1.5f) isFlashing = false;

            yield return new WaitForSeconds(Random.Range(0.25f, 0.75f));
            lightToFlash.enabled = !lightToFlash.enabled;
        }

        lightToFlash.color = originalColor;
        lightToFlash.intensity = originalIntensity;
    }
}
