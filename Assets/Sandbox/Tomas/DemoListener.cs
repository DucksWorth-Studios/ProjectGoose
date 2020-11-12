using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Author: Tomas
/// Creates a small lsitener to demo events
/// </summary>
public class DemoListener : MonoBehaviour
{
    //Add Functions to events
    void Start()
    {
        EventManager.instance.OnTestEventCall += ChangeColorWithoutSpecification;
        EventManager.instance.OnTestEventCallParam += ChangeColorSpecified;
    }

    //Change Color always to green
    private void ChangeColorWithoutSpecification()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = Color.green;
    }

    //Change to whatever is wanted
    private void ChangeColorSpecified(Color color)
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
