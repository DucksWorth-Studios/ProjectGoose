﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;


/// <summary>
/// Author: Tomas
/// Reusable Button Script. Defining the event that will be called via en um makes script reusable
/// </summary>
public class ButtonScript : MonoBehaviour
{
    [Tooltip("What Button Is Being Pressed")]
    public ButtonEnum buttonToPress;
    public void OnPress(Hand hand)
    {
        //SOUND play button click sound
        EventManager.instance.PressButton(buttonToPress);
    }
}

