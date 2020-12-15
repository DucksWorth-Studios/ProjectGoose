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
    public Text amount;

    private float amountCurrently;
    private bool IsInPast = true;
    void Start()
    {
        if(amountToGet != 0)
        {

            EventManager.instance.OnTimeJump += detectChange;
        }
    }

    //Unlock Object If Meet Criteria Set Indicator to Green
    private void unLockObjects()
    {
        lidZone.GetComponent<ItemHolder>().setInteractable();
        elementZone.GetComponent<ItemHolder>().setInteractable();
        lockIndicator.color = new Color(0,1,0);
    }

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
            IsInPast = false;
            setHalfLife(2f);
        }
    }

    //Half Life will either double or half
    private void setHalfLife(float Multiplier)
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            unLockObjects();
        }
    }
}
