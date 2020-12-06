using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demobutton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        EventManager.instance.NegatorItemJump();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
