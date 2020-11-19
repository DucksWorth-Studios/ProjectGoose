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
    private Color resultTimeJump;
    //A Test behaves as an ordinary method
    [OneTimeSetUp]
    public void CompositionManagerTestsSimplePasses()
    {
        //Need for events
        MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/_EventManager"));

        //Needed for debugging
        //Camera camera = MonoBehaviour.Instantiate(Resources.Load<Camera>("Prefabs/Main Camera"));
        //camera.transform.position = new Vector3(-1.4f, 1,-1.5f);
        //MonoBehaviour.Instantiate(Resources.Load<Light>("Prefabs/Directional Light"));

        //Needed for chemical pour to work
        floor = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Base"));
        floor.transform.position = new Vector3(0, -0.54f, 0);

        //Pouring Item position and composition
        pourObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/testTube"));
        compositionManagerPourObject = pourObject.GetComponentInChildren<CompositionManager>();
        compositionManagerPourObject.currentColor = pourColor;
        pourObject.transform.position = new Vector3(-2,1,0);

        //Recieving Item position and composition
        recieveObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/testTube"));
        compositionManagerRecieveObject = recieveObject.GetComponentInChildren<CompositionManager>();
        compositionManagerRecieveObject.currentColor = recieveColor;
        recieveObject.transform.position = new Vector3(-1.88f, 0.422f, 0.01f);

        //Result for mix test should be
        resultMix = Color.Lerp(recieveColor, pourColor, 0.5f);


        //Create the Color result that should be passed
        float hue;
        float saturation;
        float brightness;

        Color.RGBToHSV(pourColor, out hue, out saturation, out brightness);

        hue += 0.5f;

        hue = hue % 1f;

        resultTimeJump = Color.HSVToRGB(hue, saturation, brightness);
    }

    //Test collision between chemicals
    [UnityTest,Order(1)]
    public IEnumerator ChemicalMixTest()
    {
        //Rotate Object to pour into other
        pourObject.transform.Rotate(0,0,-60f,Space.World);

        yield return new WaitForSeconds(2f);

        Assert.AreEqual(resultMix, compositionManagerRecieveObject.currentColor);
    }

    //Test item changing without hand attached should do nothing
    [UnityTest, Order(2)]
    public IEnumerator TimeShiftChangeNoHandAttached()
    {
        EventManager.instance.TimeJump();

        yield return new WaitForSeconds(2f);
        //Should not change
        Assert.AreEqual(Color.blue, compositionManagerPourObject.currentColor);
    }

    //Test item changing with hand attached
    [UnityTest, Order(3)]
    public IEnumerator TimeShiftChangeHandAttached()
    {
        //Debug statement to control hand
        compositionManagerPourObject.debugHold = true;
        EventManager.instance.TimeJump();
        
        yield return new WaitForSeconds(2f);

        Assert.AreEqual(resultTimeJump, compositionManagerPourObject.currentColor);
    }
}

