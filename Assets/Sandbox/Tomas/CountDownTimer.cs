using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author: Tomas
/// Used to manage the CountDown of the 
/// </summary>
public class CountDownTimer : MonoBehaviour
{
    public float timeAllowed;
    public bool testBool;
    private float timeRemaining;
    private bool isCountingDown = true;
    private bool isCountingUp = false;
    private bool isGameOver = false;
    void Start()
    {
        timeRemaining = timeAllowed;
        //Events To Subscribe To
    }

    private void CountDown()
    {

    }

    private void CountUp()
    {
    
    }
    // Update is called once per frame
    void Update()
    {
        if(isCountingDown)
        {
            CountDown();
        }
        else if(isCountingUp)
        {
            CountUp();
        }
        else
        {
            //Do Nothing
        }
    }
}
