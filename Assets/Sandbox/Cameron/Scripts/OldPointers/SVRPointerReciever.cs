using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR.Extras;

public enum ButtonTriggers{
    Start,
    Settings,
    Quit,
    Test,
    Test2
}

public class SVRPointerReciever : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;
    public ButtonTriggers buttonTrigger;

    void Awake()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == "Cube")
        {
            Debug.Log("Cube was clicked");
        } else if (e.target.name == "Button")
        {
            ButtonClickSwitch();
        }
        else
        {
            Debug.Log(e.target.name + " Clicked");
        }
    }

    private void ButtonClickSwitch()
    {
        Debug.Log(buttonTrigger);
        switch (buttonTrigger)
        {
            case ButtonTriggers.Test:
                Debug.Log("The test has been called");
                break;
            case ButtonTriggers.Test2:
                Debug.Log("Test 2");
                break;
            default:
                Debug.LogError(buttonTrigger);
                throw new System.NotImplementedException();
        }
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        if (e.target.name == "Cube")
        {
            Debug.Log("Cube was entered");
        }
        else if (e.target.name == "Button")
        {
            Debug.Log("Button was entered");
        }
        else
        {
            Debug.Log(e.target.name + " Entered");
        }
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        if (e.target.name == "Cube")
        {
            Debug.Log("Cube was exited");
        }
        else if (e.target.name == "Button")
        {
            Debug.Log("Button was exited");
        }
        else
        {
            Debug.Log(e.target.name + "Exited");
        }
    }

    public void Test()
    {
        Debug.Log("Button clicked. WOOP WOPO");
    }
}
