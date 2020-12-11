using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudDanger : MonoBehaviour
{
    public float timeToSurvive = 15;
    private float timeRemaining;
    private bool IsPlayerInCloud = false;
    private bool isGameOver = false;
    
    void Start()
    {
        timeRemaining = timeToSurvive;
        removeAfterTime();
    }

    //If Player enters start countdown
    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayerInCloud == false && other.gameObject.tag == "Player")
        {
            IsPlayerInCloud = true;
        }
    }

    //If player exits dont countdown
    private void OnTriggerExit(Collider other)
    {
        if(IsPlayerInCloud && other.gameObject.tag == "Player")
        {
            IsPlayerInCloud = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPlayerInCloud && !isGameOver)
        {
            startCountdown();
        }
        else if(!IsPlayerInCloud && !isGameOver)
        {
            resetCountDown();
        }
    }

    //Reset the countdown
    private void resetCountDown()
    {
        if (timeRemaining < timeToSurvive)
        {
            timeRemaining += Time.deltaTime;
        }
        else
        {
            timeRemaining = timeToSurvive;
        }
    }

    //Countdown to game over
    private void startCountdown()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            //Modify Player vision
        }
        else
        {
            isGameOver = true;
            //Call Events
            EventManager.instance.LoseGame();
        }
    }

    //Remove the VFX after 30 seconds
    private void removeAfterTime()
    {
        Destroy(this.transform.gameObject, 30);
    }
    //Will be used in future feature
    private void removeAllClouds()
    {
        Destroy(this.transform.gameObject);
    }
}
