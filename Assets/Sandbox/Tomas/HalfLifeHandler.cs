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
    public GameObject lidZone;
    public GameObject elementZone;
    public Image lockIndicator;
    public Text amount;
    void Start()
    {
        
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
