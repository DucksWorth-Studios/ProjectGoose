using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class FlashLight_Controller : MonoBehaviour
{
    private Light flashLight;

    // Start is called before the first frame update
    void Start()
    {
        flashLight = GetComponent<Light>();

        flashLight.enabled = false;

        EventManager.instance.OnTimeJump += EnableLight;
    }

    private void OnDisable()
    {
        EventManager.instance.OnTimeJump -= EnableLight;
    }

    private void EnableLight()
    {
        flashLight.enabled = !flashLight.enabled;
    }
}
