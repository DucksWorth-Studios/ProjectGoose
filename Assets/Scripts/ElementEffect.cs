using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementEffect : MonoBehaviour
{
    private bool HasJumped = true;
    public bool IsCorrect = false;
    public GameObject effect;
    public float amountOfElement;
    public ItemHolder itemHolder;
    public bool IsReleased = false;
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
                    SetHalfLife(0.5f);
                    //DisableEffect
                    effect.SetActive(false);
                    IsCorrect = true;
                    print("True");
                }
                else
                {
                    //Is Going Back
                    HasJumped = false;
                    SetHalfLife(2f);
                    //Enable Effect
                    effect.SetActive(true);
                    IsCorrect = false;
                    print("False");
                }
            }
        }

    }

    //Set The amount
    private void SetHalfLife(float Multiplier)
    {
        amountOfElement = amountOfElement * Multiplier;
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
