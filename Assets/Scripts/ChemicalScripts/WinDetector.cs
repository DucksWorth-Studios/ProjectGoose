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
        
        if (other.gameObject.tag == AppData.elementTag && compositionManager.ISComposition && other.gameObject.GetComponent<ElementEffect>().IsCorrect)
        {
            compositionManager.radiation.SetActive(true);
            compositionManager.HasElement = true;
            //Send Event #

            EventManager.instance.HighlightItem(KEY.SOLUTION);
            //Detach the object
            other.gameObject.GetComponent<Valve.VR.InteractionSystem.Interactable>().attachedToHand.DetachObject(other.gameObject, true);
            //Get rid of Object
            Destroy(other.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
