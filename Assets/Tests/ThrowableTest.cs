using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// Author: Cameron Scholes
/// Tests the player dimension jump ability
/// </summary>
public class ThrowableTest
{
    private GameObject throwable;
    private Rigidbody throwableRB;

    private Throwable throwableScript;
    private VRItemDimensionJump dimensionJump;
    
    private Vector3 position;
    
    [OneTimeSetUp]
    public void Setup()
    {
        MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/_EventManager"));
        
        throwable = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/VR/Throwable"));
        position = throwable.transform.position;
        throwableScript = throwable.GetComponent<Throwable>();
        throwableRB = throwable.GetComponent<Rigidbody>();
        dimensionJump = throwable.GetComponent<VRItemDimensionJump>();
    }

    [UnityTest, Order(1)]
    public IEnumerator ThrowableTestInitialMass()
    {
        Assert.AreEqual(throwableRB.mass, dimensionJump.normalObjectMass);
        Assert.AreEqual(throwable.transform.position, position); //Gravity should not be enabled yet
        yield return null;
    }
    
    [UnityTest, Order(2)]
    public IEnumerator ThrowableTestAgedMass()
    {
        throwableScript.Attach();
        EventManager.instance.TimeJump();
        Assert.AreEqual(throwableRB.mass, dimensionJump.agedObjectMass);
        Assert.AreEqual(throwable.transform.position, position);//Gravity should not be enabled yet
        yield return null;
    }
    
    [UnityTest, Order(3)]
    public IEnumerator ThrowableTestGravityEnabled()
    {
        throwableScript.Detach();
        
        yield return new WaitForSeconds(1);
        Assert.AreNotEqual(throwable.transform.position, position);
    }
}
