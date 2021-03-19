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
        
        if (other.gameObject.tag == AppData.elementTag && compositionManager.ISComposition)
        {
            compositionManager.HasElement = true;
            //Send Event #
            EventManager.instance.HighlightItem(KEY.SOLUTION);
            //Get rid of Object
            Destroy(other.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
