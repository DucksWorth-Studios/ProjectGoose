using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CountDownTest
{
    private GameObject timerObject;
    private CountDownTimer timerScript;
        // A Test behaves as an ordinary method
    [OneTimeSetUp]
    public void CountDownTestSetUp()
    {
        MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/_EventManager"));
        timerObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Timer Prefab"));
        timerScript = timerObject.GetComponent<CountDownTimer>();
    }

        
    [UnityTest,Order(0)]
    public IEnumerator CountDownWithoutEvent()
    {
        //Nothing Happens
        yield return new WaitForSeconds(2f);
        Assert.AreEqual(20f,timerScript.getRemainingTime());
    }

    //Jump Into the Future for 10 seconds then back for 11.
    //Timer should reset to 20
    [UnityTest, Order(1)]
    public IEnumerator CountDownJumpForwardAndBack()
    {
        EventManager.instance.TimeJump();
        yield return new WaitForSeconds(10f);
        EventManager.instance.TimeJump();
        yield return new WaitForSeconds(11f);
        Assert.AreEqual(20f, timerScript.getRemainingTime());
    }

    //Start Timer. Wait 2 secs then make sure it has started
    [UnityTest, Order(3)]
    public IEnumerator CountDownIsLessThanAllowed()
    {
        EventManager.instance.TimeJump();
        yield return new WaitForSeconds(2f);
        Assert.Greater(20f,timerScript.getRemainingTime());
    }

    //Stop Timer. Wait then make sure it hasnt gone below 0
    [UnityTest, Order(4)]
    public IEnumerator CountDownIsMoreThanZero()
    {
        EventManager.instance.TimeJump();
        yield return new WaitForSeconds(0.1f);
        Assert.Greater(timerScript.getRemainingTime(), 0f);
    }

    //StartTimer and wait for it to run out.
    [UnityTest, Order(5)]
    public IEnumerator CountDownGameOver()
    {
        //Nothing Happens
        EventManager.instance.TimeJump();
        yield return new WaitForSeconds(21f);
        Assert.AreEqual(true, timerScript.isGameOver);
    }
}

