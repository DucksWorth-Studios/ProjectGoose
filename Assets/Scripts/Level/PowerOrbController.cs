using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PowerOrbController : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;

    private float t = 0;

    private bool isMovingUp = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isMovingUp)
        {
            this.transform.position = Vector3.Lerp(startPos.position, endPos.position, t);
        }
        else
        {
            this.transform.position = Vector3.Lerp(endPos.position, startPos.position, t);
        }

        t += 0.3f * Time.deltaTime;

        if (t >= 1)
        {
            t = 0;
            isMovingUp = !isMovingUp;
        }  
    }
}
