using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.OnTestEventCall += ChangeColorWithoutSpecification;
        EventManager.instance.OnTestEventCallParam += ChangeColorSpecified;
    }


    private void ChangeColorWithoutSpecification()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = Color.green;
    }

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
