using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Key Enum Used TO Control what Items Highlight
public enum KEY { CHEMICAL,USB,USBSLOT };
/// <summary>
/// Author: Tomas
/// Controls the key item highlighting throughout the game.
/// </summary>
///Requires Outline Class
[RequireComponent(typeof(Outline))]
public class KeyItem : MonoBehaviour
{
    //Was the Item Handled
    private bool WasHandled = false;
    //Is the Item Highlighted
    private bool IsHighlighted = false;

    //The Outline Class
    private Outline outline;
    //The Interactable Of The Object
    private Valve.VR.InteractionSystem.Interactable interactable;

    void Start()
    {
        outline = GetComponent<Outline>();
        interactable = GetComponent<Valve.VR.InteractionSystem.Interactable>();
    }

    private void TurnOnHighLight()
    {

    }

    private void TurnOffHighLight()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
