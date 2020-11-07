using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private Color previousColor = Color.white;
    private void Awake()
    {
        //Renderer mat = GetComponent<Renderer>();
        //mat.material.color = Color.blue;
    }
    public void switchColour(Color color)
    {
        //get current material
        Material material = GetComponent<Renderer>().material;

        // if new color is not equal old one change
        if (color != previousColor)
        {
            //mix current and new by 50% to form a new color
            material.color = Color.Lerp(material.color, color, 0.5f);
            //set as previous
            previousColor = color;
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        //print(collision.gameObject.tag);
    }
    // Update is called once per frame
    void Update()
    {

    }
}

