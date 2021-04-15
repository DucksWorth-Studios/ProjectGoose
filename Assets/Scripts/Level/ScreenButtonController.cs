using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ScreenButtonController : MonoBehaviour
{
    [Tooltip("What Button Is Being Pressed")]
    public ButtonEnum buttonToPress;

    private void HandHoverUpdate(Hand hand)
    {
        EventManager.instance.PressButton(buttonToPress);
    }
}
