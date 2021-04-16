using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementEffect : MonoBehaviour
{
    [Tooltip("Radiation Effect")]
    public GameObject effect;
    [Tooltip("The holder of the item")]
    public ItemHolder itemHolder;
    [Tooltip("Is Right amount")]
    public bool IsCorrect = false;
    [Tooltip("Has been released")]
    public bool IsReleased = false;

    //Has Been Jumped
    private bool HasJumped = true;
    //The Interactable
    private Valve.VR.InteractionSystem.Interactable interactable;
    // Start is called before the first frame update
    void Start()
    {
        effect.SetActive(false);
        interactable = GetComponent<Valve.VR.InteractionSystem.Interactable>();
        EventManager.instance.OnTimeJump += DetectChange;
    }

    private void DetectChange()
    {
        if(IsReleased)
        {
            if (interactable.attachedToHand != null)
            {
                if (!HasJumped)
                {
                    //Is Going to Future
                    HasJumped = true;

                    //DisableEffect
                    effect.SetActive(false);
                    //Is it right amount
                    IsCorrect = true;
                }
                else
                {
                    //Is Going Back
                    HasJumped = false;

                    //Enable Effect
                    effect.SetActive(true);
                    //Is it right amount
                    IsCorrect = false;
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(!itemHolder.isHolding && !IsReleased)
        {
            IsReleased = true;
            IsCorrect = true;
            print("Released");
        }
    }
}
