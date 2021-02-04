using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUpListener : MonoBehaviour
{
    [Tooltip("Select To Differentiate it for the event manager")]
    public KEY itemToListen;
    [Tooltip("Select To Differentiate it for the event manager")]
    public STAGE stage;

    private bool ShouldFire = false;
    private Valve.VR.InteractionSystem.Interactable interactable;
    void Start()
    {
        interactable = GetComponent<Valve.VR.InteractionSystem.Interactable>();
        EventManager.instance.OnItemHighlight += TurnOnHighLight;
    }

    // Update is called once per frame
    private void CheckToSendEvent()
    {
        if (interactable.attachedToHand != null)
        {
            EventManager.instance.Progress(stage);
        }
    }

    private void TurnOnHighLight(KEY item)
    {
        if (item == itemToListen)
        {
            ShouldFire = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (ShouldFire)
        {
            CheckToSendEvent();
        }
    }
}
