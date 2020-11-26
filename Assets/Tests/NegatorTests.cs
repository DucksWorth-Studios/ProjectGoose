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

        //Camera camera = MonoBehaviour.Instantiate(Resources.Load<Camera>("Prefabs/Main Camera"));
        //camera.transform.position = new Vector3(10, 0, 0);
        ////camera.transform.rotation.eulerAngles = new Vector3(0,-80,0);
        //MonoBehaviour.Instantiate(Resources.Load<Light>("Prefabs/Directional Light"));

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
        EventManager.instance.NegatorItemJump();
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

        yield return new WaitForSeconds(2f);
        EventManager.instance.NegatorItemJump();
        yield return new WaitForSeconds(2f);
        //Reciever should be empty
        bool isFree = recieverScript.isNotOccupied();
        Assert.IsTrue(isFree);
    }

    [UnityTest, Order(2)]
    public IEnumerator NegatorTestsSendOneObject()
    {


        //Get One out of Way
        objectOne.transform.position = new Vector3(0, 0, 20f);
        //Put into Sender
        objectTwo.transform.position = new Vector3(0, 0, -0.4f);
        //Send
        yield return new WaitForSeconds(2f);
        EventManager.instance.NegatorItemJump();
        yield return new WaitForSeconds(2f);
        //item should now be in Reciever
        Vector3 newPos = new Vector3(0,0,9.6f);
        Assert.AreEqual(newPos,objectTwo.transform.position);
    }

    [UnityTest, Order(3)]
    public IEnumerator NegatorTestsDontAllowWhileObjectInReciever()
    {
        //Put in Sender
        objectOne.transform.position = new Vector3(0, 0, 0.4f);
        yield return new WaitForSeconds(2f);
        //call Event
        EventManager.instance.NegatorItemJump();
        yield return new WaitForSeconds(2f);
        //Shouldnt Change
        Vector3 newPos = new Vector3(0, 0, 0.4f);
        Assert.AreEqual(newPos, objectOne.transform.position);
    }

    [UnityTest, Order(4)]
    public IEnumerator NegatorTestsSendSecond()
    {
        //Get out of Way
        objectTwo.transform.position = new Vector3(0, 0, 5f);
        //Put in Sender
        objectOne.transform.position = new Vector3(0, 0, 0.4f);
        //call Event
        yield return new WaitForSeconds(2f);

        EventManager.instance.NegatorItemJump();
        yield return new WaitForSeconds(2f);
        //Should Change
        Vector3 newPos = new Vector3(0, 0, 10.4f);
        Assert.AreEqual(newPos, objectOne.transform.position);
    }
}
