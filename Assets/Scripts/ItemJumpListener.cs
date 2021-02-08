using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemJumpListener : MonoBehaviour
{
    public KEY item;
    private bool ISActive = false;
    private Valve.VR.InteractionSystem.Interactable interactable;
    void Start()
    {
        EventManager.instance.OnItemHighlight += IsHighlighted;
        EventManager.instance.OnTimeJump += ItemJump;
        interactable = GetComponent<Valve.VR.InteractionSystem.Interactable>();
    }

    private void IsHighlighted(KEY keyEnum)
    {
        if(item == keyEnum)
        {
            ISActive = true;
        }
    }

    private void ItemJump()
    {
        if(interactable.attachedToHand != null && ISActive)
        {
            //Send Progress
            ISActive = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
