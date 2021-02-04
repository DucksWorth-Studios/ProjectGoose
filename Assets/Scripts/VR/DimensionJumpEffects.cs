using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionJumpEffects : MonoBehaviour
{
    private Color fadeColor = Color.black;
    private float fadeDuration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.OnTimeJumpButtonPressed += StartCameraFade;
        EventManager.instance.OnTimeJump += StartVFX;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
