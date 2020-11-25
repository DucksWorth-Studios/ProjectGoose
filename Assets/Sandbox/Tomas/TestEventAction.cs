using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Tomas
/// Creates a action that calls the event manager
/// </summary>
public class TestEventAction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Call the two test events
        if(Input.GetKey(KeyCode.D))
        {
            EventManager.instance.TestEventCall();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            EventManager.instance.TimeJump();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            EventManager.instance.TimeJump();
        }
    }
}
