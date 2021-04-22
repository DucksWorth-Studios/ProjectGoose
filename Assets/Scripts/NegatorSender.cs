using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Author: Tomas
/// Negator is a way to avoid the corruption from time travel. This will be used for the negator microwave machine.
/// Takes in one object at a time and transports it to the reciever. 
/// </summary>
public class NegatorSender : MonoBehaviour
{
    [FormerlySerializedAs("reciever")] [Tooltip("The Receiver Object")]
    public GameObject receiver;

    [Tooltip("The Effect Object")]
    public GameObject effect;

    private Vector3 positionDifference;
    private GameObject objectInZone;
    private int entityCount = 0;

    private void Awake()
    {
        effect.SetActive(false);
    }
    //Find the difference
    void Start()
    {
        //Find difference between the two zones
        Vector3 senderPos = this.transform.position;
        Vector3 receiverPos = receiver.transform.position;
        positionDifference = new Vector3(
          receiverPos.x - senderPos.x,
          receiverPos.y - senderPos.y,
          receiverPos.z - senderPos.z);
        EventManager.instance.OnButtonPress += SendObject;
    }

    //Send to the receiver via the difference
    private void SendObject(ButtonEnum buttonEnum)
    {
        if (buttonEnum != ButtonEnum.NEGATOR) 
            return;
        
        bool isReceiverReady = receiver.GetComponent<NegatorReciever>().isNotOccupied();
            
        if (objectInZone && entityCount == 1 && isReceiverReady)
        {
            effect.SetActive(false);
            effect.SetActive(true);

            objectInZone.transform.position += positionDifference;
            EventManager.instance.PlayOneSound(Sound.ItemTeleport, false);
        }
        else
        {
            EventManager.instance.PlayOneSound(Sound.ItemTeleport, true);
        }
    }

    //Tracks multiple objects anything coming in puts up the count.
    private void OnTriggerEnter(Collider other)
    {   
        if(!other.CompareTag(AppData.chemicalTag) && !other.CompareTag(AppData.ignoreTag) && !other.CompareTag(AppData.elementTag))
        {
            //print("Name " + other.gameObject.name);
            entityCount++;
        }
        else if(other.CompareTag(AppData.elementTag))
        {
            if(other.gameObject.GetComponent<ElementEffect>().IsReleased)
            {
                entityCount++;
            }
        }
    }
    //Anything that leaves the vount drops if the object to send leaves become null
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(AppData.chemicalTag) && !other.CompareTag(AppData.ignoreTag) && !other.CompareTag(AppData.elementTag))
        {
            entityCount--;
        }
        else if (other.CompareTag(AppData.elementTag))
        {
            // print("Element has Been Here");
            if (other.gameObject.GetComponent<ElementEffect>().IsReleased)
            {
                entityCount--;
            }
        }
        if (other.gameObject == objectInZone)
        {
            objectInZone = null;
        }
    }
    //if nothing has been set sset it as sendable object. If it is null and there us something there set it as sendable
    private void OnTriggerStay(Collider other)
    {
        
        if(entityCount == 0)
        {
            if(!other.CompareTag(AppData.chemicalTag) && !other.CompareTag(AppData.ignoreTag) && !other.CompareTag(AppData.elementTag))
            {
                GameObject obj = other.gameObject;
                LaserPonterReciever lpr = obj.GetComponent<LaserPonterReciever>();

                if (lpr)
                {
                    lpr.rigidbody.useGravity = false;
                    lpr.rigidbody.isKinematic = true;
                }

                objectInZone = obj.gameObject;
            }
            else if (other.CompareTag(AppData.elementTag))
            {
                GameObject obj = other.gameObject;
                
                if (!obj.GetComponent<ElementEffect>().IsReleased) 
                    return;

                LaserPonterReciever lpr = obj.GetComponent<LaserPonterReciever>();

                if (lpr)
                {
                    lpr.rigidbody.useGravity = false;
                    lpr.rigidbody.isKinematic = true;
                }
                
                objectInZone = obj.gameObject;
            }
        }
        else if(entityCount == 1 && objectInZone == null)
        {
            if (!other.CompareTag(AppData.chemicalTag) && !other.CompareTag(AppData.ignoreTag) && !other.CompareTag(AppData.elementTag))
            {
                GameObject obj = other.gameObject;
                LaserPonterReciever lpr = obj.GetComponent<LaserPonterReciever>();

                if (lpr)
                {
                    lpr.rigidbody.useGravity = false;
                    lpr.rigidbody.isKinematic = true;
                }
                
                objectInZone = obj.gameObject;
            }
            else if (other.CompareTag(AppData.elementTag))
            {
                GameObject obj = other.gameObject;
                
                if (!obj.GetComponent<ElementEffect>().IsReleased) 
                    return;
                
                LaserPonterReciever lpr = obj.GetComponent<LaserPonterReciever>();

                if (lpr)
                {
                    lpr.rigidbody.useGravity = false;
                    lpr.rigidbody.isKinematic = true;
                }
                
                objectInZone = obj.gameObject;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //print("Entity" + entityCount);
        //if(objectInZone != null)
        //{
        //    print(objectInZone.tag);
        //}
        if (Input.GetKeyDown(KeyCode.O))
        {
            SendObject(ButtonEnum.NEGATOR);
        }
    }
}
