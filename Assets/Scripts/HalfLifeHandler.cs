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
    public float amountOfElement;
    public float amountToGet;
    public GameObject lidZone;
    public GameObject elementZone;
    public Image lockIndicator;
    public Text textbox;

    private bool IsInPast = true;
    void Start()
    {
        //Add To Events
        EventManager.instance.OnTimeJump += detectChange;     
    }

    //Unlock Object If Meet Criteria Set Indicator to Green
    private void unLockObjects()
    {
        lidZone.GetComponent<ItemHolder>().setInteractable();
        elementZone.GetComponent<ItemHolder>().setInteractable();
        lockIndicator.color = new Color(0,1,0);
    }

    //If going in direction either half or double the amount
    private void detectChange()
    {
        if(IsInPast)
        {
            //Is Going to Future
            IsInPast = false;
            setHalfLife(0.5f);
        }
        else
        {
            //Is Going Back
            IsInPast = true;
            setHalfLife(2f);
        }
    }

    //Set The amount
    private void setHalfLife(float Multiplier)
    {
        amountOfElement = amountOfElement * Multiplier;
        detectResult();
        setAmount();
    }

    //Set the Indicator Amount
    private void setAmount()
    {
        textbox.text = "" + amountOfElement;
    }

    //detect if we reach required amount
    private void detectResult()
    {
        if(amountOfElement == amountToGet)
        {
            unLockObjects();
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.O))
    //    {
    //        EventManager.instance.TimeJump();
    //    }
    //}
}
