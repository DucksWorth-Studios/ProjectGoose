using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DecayMaterialTest
{
    private GameObject testCube;
    private DecayMaterial script; // Contains blend amount float
    private EventManager eventManager;

    // A Test behaves as an ordinary method
    [OneTimeSetUp]
    public void DecayMaterialTestSimplePasses()
    {
        var Eventobject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/_EventManager"));

        eventManager = Eventobject.GetComponent<EventManager>(); // needed to throw events

        testCube = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/DecayMaterialTest/DecayMaterialTestCube"));
        script = testCube.GetComponent<DecayMaterial>();
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest, Order(1)]
    public IEnumerator DecayMaterialTestPresentToDecayed()
    {
        script.isDecaying = true;

        yield return new WaitForSeconds(2f);

        Assert.GreaterOrEqual(script.blendAmount, 0.95f);
    }

    [UnityTest, Order(2)]
    public IEnumerator DecayMaterialTestDecayedToPresent()
    {
        script.isDecaying = true;

        yield return new WaitForSeconds(2f);

        Assert.LessOrEqual(script.blendAmount, 0.05f);
    }
}
