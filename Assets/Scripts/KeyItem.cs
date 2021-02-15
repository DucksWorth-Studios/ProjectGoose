using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Tomas
/// Controls the key item highlighting throughout the game.
/// </summary>
///Requires Outline Class
[RequireComponent(typeof(Outline))]
public class KeyItem : MonoBehaviour
{
    [Tooltip("Select To Differentiate it for the event manager")]
    public KEY itemToListen;
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
        outline.enabled = false;
        interactable = GetComponent<Valve.VR.InteractionSystem.Interactable>();
        EventManager.instance.OnItemHighlight += TurnOnHighLight;
    }

    /// <summary>
    /// Takes in the event parmaeter and checks against stored param if match it allows highlight
    /// </summary>
    /// <param name="item"></param>
    private void TurnOnHighLight(KEY item)
    {
        if(item == itemToListen)
        {
            outline.enabled = true;
            IsHighlighted = true;
        }
    }

    /// <summary>
    /// Check of The Highlight is needed to turn off
    /// </summary>
    private void CheckToTurnOFF()
    {
        if (interactable.attachedToHand != null)
        {
            IsHighlighted = false;
            outline.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(IsHighlighted)
        {
            CheckToTurnOFF();
        }
    }
}
