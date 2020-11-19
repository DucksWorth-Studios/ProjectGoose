using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class CompositionManagerTests
{
    //Pouring Object
    private GameObject pourObject;
    private CompositionManager compositionManagerPourObject;
    private Color pourColor = Color.blue;

    //Recieveing Object
    private GameObject recieveObject;
    private CompositionManager compositionManagerRecieveObject;
    private Color recieveColor = Color.red;

    private GameObject floor;
    private Color resultMix;
    private Color resultTimeJump = new Color(1f, 0f, 0f);
    //A Test behaves as an ordinary method
    [OneTimeSetUp]
    public void CompositionManagerTestsSimplePasses()
    {
        MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/_EventManager"));
        Camera camera = MonoBehaviour.Instantiate(Resources.Load<Camera>("Prefabs/Main Camera"));

        camera.transform.position = new Vector3(-1.4f, 1,-1.5f);
        MonoBehaviour.Instantiate(Resources.Load<Light>("Prefabs/Directional Light"));

        floor = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Base"));
        floor.transform.position = new Vector3(0, -0.54f, 0);

        pourObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/testTube"));
        compositionManagerPourObject = pourObject.GetComponentInChildren<CompositionManager>();
        compositionManagerPourObject.currentColor = pourColor;
        pourObject.transform.position = new Vector3(-2,1,0);

        recieveObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/testTube"));
        compositionManagerRecieveObject = recieveObject.GetComponentInChildren<CompositionManager>();
        compositionManagerRecieveObject.currentColor = recieveColor;
        recieveObject.transform.position = new Vector3(-1.88f, 0.422f, 0.01f);

        resultMix = Color.Lerp(recieveColor, pourColor, 0.5f);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest,Order(1)]
    public IEnumerator CompositionManagerTestsWithEnumeratorPasses()
    {
        pourObject.transform.Rotate(0,0,-60f,Space.World);

        yield return new WaitForSeconds(2f);

        Assert.AreEqual(resultMix, compositionManagerRecieveObject.currentColor);
    }

    //[UnityTest, Order(2)]
    //public IEnumerator TimeShiftChange()
    //{
    //    EventManager.instance.TimeJump();

    //    yield return new WaitForSeconds(1f);

    //    Assert.AreEqual(resultTimeJump, compositionManagerRecieveObject.currentColor);
    //}
}

