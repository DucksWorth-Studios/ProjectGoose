using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositionManager : MonoBehaviour
{
    public Color currentColor = Color.white;
    private Color previousColor = Color.white;
    private Material currentMaterial = null;
    private void Awake()
    {
        //Allows access to material
        currentMaterial = GetComponent<Renderer>().material;
        //Make sure composition matches what its set to.
        currentColor = currentMaterial.color;
        //Save what it is currenty/ Wont trigger if they match later
        previousColor = currentColor;
    }

    void Start()
    {

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
        float hue;
        float saturation;
        float brightness;

        Color.RGBToHSV(currentColor,out hue,out saturation,out brightness);
        print("Hue: " + hue);
        hue += 0.5f;
        hue = hue % 1f;
        print("Hue: " + hue);
        Color boi = Color.HSVToRGB(hue,saturation,brightness);

        currentMaterial.color = boi;
        print("Hello");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            timeShiftChange();
        }
    }
}
