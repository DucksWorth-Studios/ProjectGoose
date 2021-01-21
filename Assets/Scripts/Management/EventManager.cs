using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author: Tomas
/// Simple Event System for the Game.
/// Created as a Singleton as it will only need to be created once in the game.
/// </summary>
public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    private void Awake()
    {
        //Creates a singleton
        instance = this;
    }
    //All events stored here
    public event Action OnTestEventCall;
    public event Action<Color> OnTestEventCallParam;
    public event Action OnTimeJump;
    public event Action OnNegatorItemJump;
    public event Action<Sound> OnPlaySound;
    public event Action<Sound> OnStopSound;
    public event Action<Sound,bool> OnPlayOneSound;
    public event Action OnLoseGame;
    public event Action OnWinGame;
    public event Action<ButtonEnum> OnButtonPress;
    public event Action<Snap> OnItemSnap;
    //This is a model for an event
    public void TestEventCall()
    {
        // we can use ?.invoke to call our events but for debug reasons this allows quick access
        if (OnTestEventCall != null)
        {
            OnTestEventCall();
        }
        else
        {
            Debug.Log("testEventCall is Null");
        }
    }

    //Simple Demo of Passing Params
    public void TestEventCallParam(Color color)
    {
        if (OnTestEventCall != null)
        {
            OnTestEventCallParam(color);
        }
        else
        {
            Debug.Log("testEventCallParam is Null");
        }
    }


    public void TimeJump()
    {
        OnTimeJump?.Invoke();
    }

    public void NegatorItemJump()
    {
        if (OnNegatorItemJump != null)
        {
            OnNegatorItemJump();
        }
        else
        {
            Debug.Log("Time Jump is Null");
        }
    }

    public void PlaySound(Sound audio)
    {
        
        if (OnPlaySound != null)
        {
            OnPlaySound(audio);
        }
        else
        {
            Debug.Log("PlaySound is Null");
        }
    }

    public void StopSound(Sound audio)
    {

        if (OnStopSound != null)
        {
            OnStopSound(audio);
        }
        else
        {
            Debug.Log("OnStopSound is Null");
        }
    }

    public void LoseGame()
    {
        if (OnLoseGame != null)
        {
            OnLoseGame();
        }
        else
        {
            Debug.Log("LoseGame is Null");
        }
    }

    public void WinGame()
    {
        if (OnWinGame != null)
        {
            OnWinGame();
        }
        else
        {
            Debug.Log("WinGame is Null");
        }
    }

    public void PressButton(ButtonEnum buttonToPress)
    {
        if (OnButtonPress != null)
        {
            OnButtonPress(buttonToPress);
        }
        else
        {
            Debug.Log("OnButtonPress is Null");
        }
    }

    public void SnappedItem(Snap itemSnapped)
    {
        if (OnItemSnap != null)
        {
            OnItemSnap(itemSnapped);
        }
        else
        {
            Debug.Log("OnItemSnap is Null");
        }
    }

    public void PlayOneSound(Sound sound, bool firstSound)
    {
        if (OnPlayOneSound != null)
        {
            OnPlayOneSound(sound,firstSound);
        }
        else
        {
            Debug.Log("OnPlayOneSound is Null");
        }
    }
}

