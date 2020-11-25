using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTimer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print("HERE");
    }

    private void OnTriggerExit(Collider other)
    {
        print("Out");
    }
    // Update is called once per frame
    void Update()
    {
        //print("Hello");
    }
}
