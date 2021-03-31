using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PowerOrbController : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;

    public float startDelay = 0;

    private float t = 0;

    private bool isMovingUp = true;
    private bool canMove = false;

    // Start is called before the first frame update
    void Start()
    {
        if (startDelay > 0)
            StartCoroutine(WaitToMove(startDelay));
        else
            canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;

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

    IEnumerator WaitToMove(float delay)
    {
        yield return new WaitForSeconds(delay);

        canMove = true;
    }
}
