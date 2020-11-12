using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEventAction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            EventManager.instance.TestEventCall();
        }

        if (Input.GetKey(KeyCode.S))
        {
            EventManager.instance.TestEventCallParam(Color.black);
        }

        if (Input.GetKey(KeyCode.A))
        {
            EventManager.instance.TestEventCallParam(Color.blue);
        }
    }
}
