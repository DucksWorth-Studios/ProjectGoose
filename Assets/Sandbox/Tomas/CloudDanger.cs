using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudDanger : MonoBehaviour
{
    public bool IsPlayerInCloud = false;
    private bool isGameOver = false;
    public float timeToSurvive = 15;
    public float timeRemaining;
    void Start()
    {
        timeRemaining = timeToSurvive;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayerInCloud == false && other.gameObject.tag == "Player")
        {
            IsPlayerInCloud = true;
        }
    }

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
}
