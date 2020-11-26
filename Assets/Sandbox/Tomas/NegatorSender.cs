using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegatorSender : MonoBehaviour
{
    public GameObject reciever;
    private Vector3 positionDifference;
    private GameObject objectInZone;
    private int entityCount = 0;
    void Start()
    {
        //Find difference between the two zones
        Vector3 senderPos = this.transform.position;
        Vector3 recieverPos = reciever.transform.position;
        positionDifference = new Vector3(
          recieverPos.x - senderPos.x,
          recieverPos.y - senderPos.y,
          recieverPos.z - senderPos.z);



    }


    private void SendObject()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        entityCount++;
    }

    private void OnTriggerExit(Collider other)
    {
        entityCount--;
        if(other.gameObject == objectInZone)
        {
            objectInZone = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(entityCount == 0)
        {
            objectInZone = other.gameObject;
        }
        else if(entityCount == 1 && objectInZone == null)
        {
            objectInZone = other.gameObject;
        }
    }

    private bool isObjectInZone()
    {
        return false;
    }

    private bool AreMultipleObjectsInZone()
    {
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
