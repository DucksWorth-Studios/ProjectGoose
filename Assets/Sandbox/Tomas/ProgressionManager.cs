using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Progression { };
public class ProgressionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Possibly Link To An Event
    }

    //Outside forces dictate what stage it is at.
    //It then calls up the relevant stage
    public void Progress(Progression stage)
    {
        switch(stage)
        {
            default:
                break;
        }
    }

    private void StageOne()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
