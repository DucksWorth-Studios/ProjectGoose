using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author: Tomas
/// Manages the composition of the vials.
/// Changes it based on the time shifting and mixing of other chemicals
/// </summary>
public class CompositionManager : MonoBehaviour
{
    public Color currentColor = Color.white;
    private Color previousColor = Color.white;
    private Material currentMaterial = null;
    private Valve.VR.InteractionSystem.Interactable interactable = null;
    public bool debugHold = false;
    private void Awake()
    {
        //Get the interactable component
        interactable = GetComponentInParent<Valve.VR.InteractionSystem.Interactable>();

        //Allows access to material
        currentMaterial = GetComponent<Renderer>().material;

        //Make sure composition matches what its set to.
        currentMaterial.color = currentColor;

        //Save what it is currenty/ Wont trigger if they match later
        previousColor = currentColor;

        //Sets the timeSHift method to only call when time is jumped
        EventManager.instance.OnTimeJump += timeShiftChange;
    }

    public void mixChemical(Color chemicalAdditive)
    {
        if(chemicalAdditive != previousColor)
        {
            //Mix the chemicals by 50%
            currentColor = Color.Lerp(chemicalAdditive,currentColor,0.5f);
            currentMaterial.color = currentColor;

            //Ensures only changed once else it will continuesly chnage the material until it becomes the additive
            previousColor = chemicalAdditive;
        }
    }

    public void timeShiftChange()
    {
        //Only will trigger if attached to hand
        if(interactable.attachedToHand != null || debugHold)
        {
            //Values to modify
            float hue;
            float saturation;
            float brightness;

            //Get the Hue Saturation and Brightness
            Color.RGBToHSV(currentColor, out hue, out saturation, out brightness);

            //Modifying the hue by 0.5 will change its color to opposite
            hue += 0.5f;

            //Bring it back in range of 0-1
            hue = hue % 1f;

            //Recreate the Color
            Color changedColor = Color.HSVToRGB(hue, saturation, brightness);

            //Set as current
            currentMaterial.color = changedColor;
            currentColor = changedColor;
        }
    }


    public void callColorChange(GameObject otherVial)
    {
        //GameObject will be validated before this method is called
        //Take the second vial and mix it using the mix chemicalMethod.
        otherVial.GetComponent<CompositionManager>().mixChemical(currentColor);
    }


    void Update()
    {

    }
}
