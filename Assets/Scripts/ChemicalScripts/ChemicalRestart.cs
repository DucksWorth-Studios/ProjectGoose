using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author: Tomas
/// Used To Restart the Chemicals Composition
/// </summary>
public class ChemicalRestart : MonoBehaviour
{
    public CompositionManager compositionManager;
    private ButtonEnum buttonEnum = ButtonEnum.RESET;
    private Color startingColor;
    void Start()
    {
        //Create temp color so a reference is not created
        Color tempColor = compositionManager.currentColor;
        float tempR = tempColor.r;
        float tempG = tempColor.g; 
        float tempB = tempColor.b;
        startingColor = new Color(tempR, tempG, tempB);

        EventManager.instance.OnButtonPress += ResetColors;
    }

    private void ResetColors(ButtonEnum button)
    {
        //if reset is pressed call reset function
        if(button == ButtonEnum.RESET)
        {
            compositionManager.ResetComposition(startingColor);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //print("COLOR" + startingColor);
    }
}
