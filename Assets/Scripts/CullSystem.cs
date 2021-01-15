using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullSystem : MonoBehaviour
{
    public GameObject[] presentLevelAssets;
    public GameObject[] futureLevelAssets;

    private bool inFuture = true;

    private void Start()
    {
        if (presentLevelAssets.Length < 1)
            Debug.LogError("Present Level Assets missing for Cull System");
        
        if (futureLevelAssets.Length < 1)
            Debug.LogError("Future Level Assets missing for Cull System");

        EventManager.instance.OnTimeJump += SwitchLevelVisibility;
        SwitchLevelVisibility();
    }

    private void SwitchLevelVisibility()
    {
        inFuture = !inFuture;
        
        foreach (GameObject asset in futureLevelAssets)
                asset.SetActive(inFuture);
            
        foreach (GameObject asset in presentLevelAssets)
            asset.SetActive(!inFuture);
    }
}
