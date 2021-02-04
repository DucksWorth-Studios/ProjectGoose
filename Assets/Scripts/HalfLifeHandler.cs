using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Author: tomas
/// Handles The Half Life Game Object
/// </summary>
public class HalfLifeHandler : MonoBehaviour
{
    [Tooltip("The Amount of the Element.")]
    public float amountOfElement;
    [Tooltip("The Amount of the Element needed. Set to 0 if not important")]
    public float amountToGet;
    [Tooltip("Holder For Lid")]
    public GameObject lidZone;
    [Tooltip("Holder For Element")]
    public GameObject elementZone;
    [Tooltip("Lock Idicator(Red Or Green)")]
    public Image lockIndicator;
    [Tooltip("TextBox For Element")]
    public Text textbox;

    private bool IsInPast = true;
    private bool IsDone = false;
    void Start()
    {
        //Add To Events
        EventManager.instance.OnTimeJump += DetectChange;     
    }

    //Unlock Object If Meet Criteria Set Indicator to Green
    private void UnLockObjects()
    {
        lidZone.GetComponent<ItemHolder>().SetInteractable();
        elementZone.GetComponent<ItemHolder>().SetInteractable();
        lockIndicator.color = new Color(0,1,0);
    }

    private void LockObjects()
    {
        if(!IsDone)
        {
            lidZone.GetComponent<ItemHolder>().LockInteractable();
            elementZone.GetComponent<ItemHolder>().LockInteractable();
            lockIndicator.color = new Color(1, 0, 0);
        }
    }

    //If going in direction either half or double the amount
    private void DetectChange()
    {
        if(IsInPast)
        {
            //Is Going to Future
            IsInPast = false;
            SetHalfLife(0.5f);
        }
        else
        {
            //Is Going Back
            IsInPast = true;
            SetHalfLife(2f);
        }
    }

    //Set The amount
    private void SetHalfLife(float Multiplier)
    {
        amountOfElement = amountOfElement * Multiplier;
        DetectResult();
        SetAmount();
    }

    //Set the Indicator Amount
    private void SetAmount()
    {
        textbox.text = "" + amountOfElement;
    }

    //detect if we reach required amount
    private void DetectResult()
    {
        if(amountOfElement == amountToGet)
        {
            UnLockObjects();
        }
        else
        {
            LockObjects();
        }
    }


    void Update()
    {
        if (!IsDone)
        {
            if(!lidZone.GetComponent<ItemHolder>().isHolding)
            {
                textbox.text = "0";
                IsDone = true;
            }
        }
    }
}
