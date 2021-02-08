using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author: Tomas
/// Main focus is negator reciever. This just validates if anything is in the zone or not
/// </summary>
public class NegatorReciever : MonoBehaviour
{
    // Start is called before the first frame update
    private int entityCount;
    void Start()
    {
        
    }
    //Anything enters count goes up
    private void OnTriggerEnter(Collider other)
    {
        entityCount++;
    }
    //Anything leaves count goes down
    private void OnTriggerExit(Collider other)
    {
        entityCount--;
    }
    //if is empty count will be zero. You cant go below zero
    public bool isNotOccupied()
    {
        return entityCount == 0;
    }
    // Update is called once per frame
    void Update()
    {
        print("wooo" +entityCount);
    }
}
