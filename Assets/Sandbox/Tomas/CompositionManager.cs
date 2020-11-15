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
        currentMaterial = GetComponent<Material>();
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

    }

    void Update()
    {

    }
}
