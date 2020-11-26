using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegatorReciever : MonoBehaviour
{
    // Start is called before the first frame update
    private int entityCount;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        entityCount++;
    }

    private void OnTriggerExit(Collider other)
    {
        entityCount--;
    }
    public bool isNotOccupied()
    {
        return entityCount == 0;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
