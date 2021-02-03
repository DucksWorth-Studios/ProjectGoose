using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class CloudDanger : MonoBehaviour
{
    [Tooltip("The time it takes to kill a player")]
    public float timeToSurvive = 15;
    //Remaining time
    private float timeRemaining;
    //Is Player Inside
    private bool IsPlayerInCloud = false;
    //Has Game Over been triggered
    private bool isGameOver = false;
    //The VFX
    private VisualEffect cloud;
    private void Awake()
    {
        cloud = GetComponent<VisualEffect>();
    }

    //On object start begin its lifespan
    void Start()
    {
        EventManager.instance.OnButtonPress += OnRemoveCloudsEvent;
        timeRemaining = timeToSurvive;
        EventManager.instance.PlaySound(Sound.Alarm);
        StartCoroutine(AfterTimePass(27));
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
            StartCountdown();
        }
        else if(!IsPlayerInCloud && !isGameOver)
        {
            ResetCountDown();
        }
    }

    //Reset the countdown
    private void ResetCountDown()
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
    private void StartCountdown()
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
    //Used to Stop the cloud after 30 seconds
    IEnumerator AfterTimePass(float timeBefore)
    {
        //Wait then set rate to 0 this tapers off the effect
        yield return new WaitForSeconds(timeBefore);
        cloud.SetInt("Rate",0);
        //Destory object
        yield return new WaitForSeconds(3);
        Destroy(this.transform.gameObject);
        EventManager.instance.StopSound(Sound.Alarm);
    }

    //Used for Fan to Blow out smoke
    private void OnRemoveCloudsEvent(ButtonEnum buttonPress)
    {
        if(buttonPress == ButtonEnum.CLOUDREMOVE)
        {
            StopCoroutine(AfterTimePass(27));
            StartCoroutine(AfterTimePass(0));
            //Remove Event So No Errors Occur
            EventManager.instance.OnButtonPress -= OnRemoveCloudsEvent;
        }
    }
}
