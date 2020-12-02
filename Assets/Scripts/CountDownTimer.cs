using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Author: Tomas
/// Used to manage the CountDown of the 
/// </summary>
public class CountDownTimer : MonoBehaviour
{
    public float timeAllowed;
    public bool isGameOver = false;
    public GameObject countDownObject;
    public GameObject textObject;
    private bool isCountingDown = false;
    private bool isCountingUp = false;
    private bool hasReachedNormal = true;
    private float timeRemaining;
    private Image timerBar;
    void Start()
    {
        timerBar = countDownObject.GetComponent<Image>();
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
            hasReachedNormal = false;
        }
        else if(isCountingUp || hasReachedNormal)
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
            hasReachedNormal = true;
            isCountingUp = false;
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
            //print("Down" + timeRemaining);
        }
        else if(isCountingUp)
        {
            //print("Up");
            CountUp();
            //print("Up"+timeRemaining);
        }
        else
        {
            //Do Nothing
        }
        changeBar();
    }

    private void changeText()
    {
        
    }

    private void changeBar()
    {
        timerBar.fillAmount = timeRemaining / timeAllowed;
    }
}
