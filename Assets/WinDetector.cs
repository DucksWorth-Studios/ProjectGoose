using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinDetector : MonoBehaviour
{
    public CompositionManager compositionManager;
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        print("HELLO HONEY" + other.tag);
        if (other.gameObject.tag == "Element" && compositionManager.ISComposition)
        {
            compositionManager.HasElement = true;
            //Send Event 
            //Get rid of Object
            Destroy(other.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
