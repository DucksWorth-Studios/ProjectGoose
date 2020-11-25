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


    [UnityTest, Order(1)]
    public IEnumerator CountDownJumpForwardAndBack()
    {
        EventManager.instance.TimeJump();
        yield return new WaitForSeconds(10f);
        EventManager.instance.TimeJump();
        yield return new WaitForSeconds(11f);
        Assert.AreEqual(20f, timerScript.getRemainingTime());
    }

    [UnityTest, Order(3)]
    public IEnumerator CountDownIsLessThanAllowed()
    {
        EventManager.instance.TimeJump();
        yield return new WaitForSeconds(2f);
        Assert.Greater(20f,timerScript.getRemainingTime());
    }

    [UnityTest, Order(4)]
    public IEnumerator CountDownIsMoreThanZero()
    {
        EventManager.instance.TimeJump();
        yield return new WaitForSeconds(0.1f);
        Assert.Greater(timerScript.getRemainingTime(), 0f);
    }

    [UnityTest, Order(5)]
    public IEnumerator CountDownGameOver()
    {
        //Nothing Happens
        EventManager.instance.TimeJump();
        yield return new WaitForSeconds(21f);
        Assert.AreEqual(true, timerScript.isGameOver);
    }
}

