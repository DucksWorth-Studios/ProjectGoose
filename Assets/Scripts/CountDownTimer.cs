using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Author: Tomas
/// Used to manage the CountDown of the Watch
/// </summary>
public class CountDownTimer : MonoBehaviour
{
    [Tooltip("Time Takes For End")]
    public float timeAllowed;

    [Tooltip("Is Game Over")]
    public bool isGameOver = false;

    [Tooltip("Object With Count Down Image")]
    public GameObject countDownObject;

    [Tooltip("Object With Text")]
    public GameObject textObject;
    //Private Variables
    private bool isCountingDown = false;
    private bool isCountingUp = false;
    private bool fireOnce = false;
    private bool hasReachedNormal = true;
    private float timeRemaining;
    private Image timerBar;
    private Text textbox;

    void Start()
    {
        timerBar = countDownObject.GetComponent<Image>();
        textbox = textObject.GetComponent<Text>();
        timeRemaining = timeAllowed;
        //Events To Subscribe To
        EventManager.instance.OnTimeJump += TimeJumpListener;
    }

    //Listens For Time Jump
    private void TimeJumpListener()
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
    //Counts watch down
    private void CountDown()
    {
        if (Mathf.Round(timeRemaining) == 5f && !fireOnce)
        {
            EventManager.instance.PlaySound(Sound.Timer);
            fireOnce = true;
        }


        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            isCountingDown = false;
            isGameOver = true;
            //Send Event For Game Over
            EventManager.instance.LoseGame();
        }
    }

    //Counts watch Up
    private void CountUp()
    {
        if(fireOnce)
        {
            EventManager.instance.StopSound(Sound.Timer);
            fireOnce = false;
        }

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

    //Get Remaining time
    public float GetRemainingTime()
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
        ChangeUI();
    }


    //Updates UI based on time remaining v time given
    private void ChangeUI()
    {
        timerBar.fillAmount = timeRemaining / timeAllowed;
        textbox.text = "" + Mathf.Round(timeRemaining);
    }
}
