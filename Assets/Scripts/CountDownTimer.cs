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
    private Text textbox;
    void Start()
    {
        timerBar = countDownObject.GetComponent<Image>();
        textbox = textObject.GetComponent<Text>();
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
            EventManager.instance.LoseGame();
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
        changeUI();
    }



    private void changeUI()
    {
        timerBar.fillAmount = timeRemaining / timeAllowed;
        textbox.text = "" + Mathf.Round(timeRemaining);
    }
}
