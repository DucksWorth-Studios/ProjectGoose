using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingController : MonoBehaviour
{
    [Tooltip("An array of lights that can flash at random points")]
    public Light[] lights;

    public bool canExecute = false;
    private bool isFlashing = false;

    private float startTime;

    private float nextPlayTime;

    public Color originalColor;
    public float originalIntensity;
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
            else if(startTime + nextPlayTime <= Time.time || startTime == 0)
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
        lightToFlash.color = Color.white;
        lightToFlash.intensity = 5;

        while(isFlashing)
        {
            yield return new WaitForSeconds(Random.Range(0.25f, 0.75f));

            if (Time.time - startTime > 1.5f)
            {
                lightToFlash.enabled = true;
                lightToFlash.color = originalColor;
                lightToFlash.intensity = originalIntensity;

                startTime = Time.time;
                nextPlayTime = Random.Range(2.5f, 6f); // Next light will flash between 2.5 and 6 seconds
                isFlashing = false;
            }

            lightToFlash.enabled = !lightToFlash.enabled;
        }
    }
}
