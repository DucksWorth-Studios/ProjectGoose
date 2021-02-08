using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author: Tomas
/// Negator is a way to avoid the corruption from time travel. This will be used for the negator microwave machine.
/// Takes in one object at a time and transports it to the reciever. 
/// </summary>
public class NegatorSender : MonoBehaviour
{
    [Tooltip("The Reciever Object")]
    public GameObject reciever;

    private Vector3 positionDifference;
    private GameObject objectInZone;
    private int entityCount = 0;

    private void Awake()
    {

    }
    //Find the difference
    void Start()
    {
        //Find difference between the two zones
        Vector3 senderPos = this.transform.position;
        Vector3 recieverPos = reciever.transform.position;
        positionDifference = new Vector3(
          recieverPos.x - senderPos.x,
          recieverPos.y - senderPos.y,
          recieverPos.z - senderPos.z);
        EventManager.instance.OnButtonPress += SendObject;
    }

    //Send to the reciever via the difference
    private void SendObject(ButtonEnum buttonEnum)
    {
        if(buttonEnum == ButtonEnum.NEGATOR)
        {
            bool isRecieverReady = reciever.GetComponent<NegatorReciever>().isNotOccupied();
            if (objectInZone != null && entityCount == 1 && isRecieverReady)
            {
                objectInZone.transform.position += positionDifference;
            }
            else
            {
                EventManager.instance.PlayOneSound(Sound.ItemTeleport, true);
            }
        }
    }

    //Tracks multiple objects anything coming in puts up the count.
    private void OnTriggerEnter(Collider other)
    {   
        if(other.tag != "Chemical" && other.tag != "Ignore")
        {
            entityCount++;
        }
    }
    //Anything that leaves the vount drops if the object to send leaves become null
    private void OnTriggerExit(Collider other)
    {
        entityCount--;
        if(other.gameObject == objectInZone)
        {
            objectInZone = null;
        }
    }
    //if nothing has been set sset it as sendable object. If it is null and there us something there set it as sendable
    private void OnTriggerStay(Collider other)
    {
        print(other.gameObject.name);
        if(entityCount == 0)
        {
            if(other.tag != "Chemical" && other.tag != "Ignore")
            {
                objectInZone = other.gameObject;
            }
        }
        else if(entityCount == 1 && objectInZone == null)
        {
            if (other.tag != "Chemical" && other.tag != "Ignore")
            {
                objectInZone = other.gameObject;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        print("Entity" + entityCount);
        if(objectInZone != null)
        {
            print(objectInZone.tag);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SendObject(ButtonEnum.NEGATOR);
        }
    }
}
