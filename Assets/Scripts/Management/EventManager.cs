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
}
