using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class NegatorTests
{

    private GameObject sender;
    private NegatorSender senderScript;
    private GameObject receiver;
    private NegatorReciever recieverScript;

    private GameObject objectOne;
    private GameObject objectTwo;
    private Vector3 distanceChanged;

    // A Test behaves as an ordinary method
    [OneTimeSetUp]
    public void NegatorTestsSimplePasses()
    {
        MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/_EventManager"));

        receiver = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Reciever"));
        recieverScript = receiver.GetComponent<NegatorReciever>();

        sender = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Sender"));
        senderScript = sender.GetComponent<NegatorSender>();
        senderScript.reciever = receiver;



        objectOne = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/testObject"));
        objectTwo = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/testObject"));

        receiver.transform.position = new Vector3(0, 0, 10);
        sender.transform.position = new Vector3(0, 0, 0);

        objectOne.transform.position = new Vector3(0, 0, -10);
        objectTwo.transform.position = new Vector3(0, 0, -5);

    }

    [UnityTest, Order(0)]
    public IEnumerator NegatorTestsWithNothing()
    {
        //Do nothing
        yield return new WaitForSeconds(2f);
        //Reciever should be empty
        bool isFree = recieverScript.isNotOccupied();
        Assert.IsTrue(isFree);
    }

    [UnityTest, Order(1)]
    public IEnumerator NegatorTestsSenderMultiObject()
    {
        objectOne.transform.position = new Vector3(0,0,0.4f);
        objectTwo.transform.position = new Vector3(0, 0, -0.4f);
        EventManager.instance.NegatorItemJump();
        yield return new WaitForSeconds(2f);
        //Reciever should be empty
        bool isFree = recieverScript.isNotOccupied();
        Assert.IsTrue(isFree);
    }

    //[UnityTest, Order(1)]
    public IEnumerator NegatorTestsSendOneObject()
    {
        objectOne.transform.position = new Vector3(0, 0, 5f);
        objectOne.transform.position = new Vector3(0, 0, -0.4f);
        EventManager.instance.NegatorItemJump();
        yield return new WaitForSeconds(2f);
        //Reciever should be empty
        Vector3 newPos = new Vector3(0,0,9.6f);
        Assert.AreEqual();
    }

}
