using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class CloudDanger : MonoBehaviour
{
    public float timeToSurvive = 15;
    private float timeRemaining;
    private bool IsPlayerInCloud = false;
    private bool isGameOver = false;
    private VisualEffect cloud;
    private void Awake()
    {
        
        cloud = GetComponent<VisualEffect>();
    }
    void Start()
    {
        EventManager.instance.OnButtonPress += OnRemoveCloudsEvent;
        timeRemaining = timeToSurvive;
        StartCoroutine(afterTimePass(27));
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
    //Used to Stop the cloud after 30 seconds
    IEnumerator afterTimePass(float timeBefore)
    {
        //Wait then set rate to 0 this tapers off the effect
        yield return new WaitForSeconds(timeBefore);
        cloud.SetInt("Rate",0);
        //Destory object
        yield return new WaitForSeconds(3);
        Destroy(this.transform.gameObject);

    }

    //Used for Fan to Blow out smoke
    private void OnRemoveCloudsEvent(ButtonEnum buttonPress)
    {
        if(buttonPress == ButtonEnum.CLOUDREMOVE)
        {
            StopCoroutine(afterTimePass(27));
            StartCoroutine(afterTimePass(0));
            //Remove Event So No Errors Occur
            EventManager.instance.OnButtonPress -= OnRemoveCloudsEvent;
        }
    }
}
