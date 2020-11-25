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
    public bool isGameOver = false;

    private bool isCountingDown = true;
    private bool isCountingUp = false;
    private float timeRemaining;
    void Start()
    {
        timeRemaining = timeAllowed;
        //Events To Subscribe To
        EventManager.instance.OnTimeJump += timeJumpListener;
    }

    private void timeJumpListener()
    {
        if(isCountingDown)
        {
            isCountingDown = false;
            isCountingUp = true;
        }
        else if(isCountingUp)
        {
            isCountingDown = true;
            isCountingUp = false;
        }
    }

    private void CountDown()
    {
        if(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            isCountingDown = false;
            isGameOver = true;
            //Send Event For Game Over
        }
    }

    private void CountUp()
    {
        if(timeRemaining < timeAllowed)
        {
            timeRemaining += Time.deltaTime;
        }
        else
        {
            timeRemaining = timeAllowed;
            //Cant  Do this otherwise will break
            //isCountingUp = false;
            //Send Event For Menu Bar Full Sound
        }
    }

    public float getRemainingTime()
    {
        return this.timeRemaining;
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
        print(timeRemaining);
    }
}
