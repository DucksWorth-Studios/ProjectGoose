using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VialZoneHighlight : MonoBehaviour
{
    [Tooltip("Select To Differentiate it for the event manager")]
    public KEY itemToListen;

    private Outline outline;
    private bool IsHighlighted = false;
    private VialZone snapZone;
    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
        snapZone = GetComponent<VialZone>();
        EventManager.instance.OnItemHighlight += TurnOnHighLight;
    }

    /// <summary>
    /// Takes in the event parmaeter and checks against stored param if match it allows highlight
    /// </summary>
    /// <param name="item"></param>
    private void TurnOnHighLight(KEY item)
    {
        if (item == itemToListen)
        {
            outline.enabled = true;
            IsHighlighted = true;
        }
    }

    /// <summary>
    /// Check of The Highlight is needed to turn off
    /// </summary>
    private void TurnOFFHighLight()
    {
        if (snapZone.isSnapped)
        {
            IsHighlighted = false;
            outline.enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (IsHighlighted)
        {
            TurnOFFHighLight();
        }
    }
}
