using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author: Tomas
/// Used To Highlight Snap Zones for the Objectives
/// </summary>
[RequireComponent(typeof(Outline))]
public class KeyZone : MonoBehaviour
{
    [Tooltip("Select To Differentiate it for the event manager")]
    public KEY itemToListen;

    private Outline outline;
    private bool IsHighlighted = false;
    private SnapZonePermanent snapZone;
    void Start()
    {
        outline = GetComponent<Outline>();
        snapZone = GetComponent<SnapZonePermanent>();
    }
    private void TurnOnHighLight(KEY item)
    {

    }

    private void TurnOFFHighLight()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if(IsHighlighted)
        {

        }
    }
}
