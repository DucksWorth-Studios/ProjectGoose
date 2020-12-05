using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class LaserPointerTest
{
    private GameObject laserPointer;
    private GameObject laserPointerReciever;
    
    private LaserPointer laserPointerScript;
    private LaserPointerReciever laserPointerRecieverScript;

    private MeshRenderer lprMeshRender;
    
    [OneTimeSetUp]
    public void Setup()
    {
        laserPointer = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/VR/LaserPointerTest"));
        laserPointerReciever = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/VR/LaserPointerReciever"));

        laserPointer.transform.position = new Vector3(0, 0, -0.5f);
        
        laserPointerScript = laserPointer.GetComponent<LaserPointer>();
        laserPointerRecieverScript = laserPointerReciever.GetComponent<LaserPointerReciever>();

        lprMeshRender = laserPointerReciever.GetComponent<MeshRenderer>();
    }

    [UnityTest, Order(1)]
    public IEnumerator LaserPointerTestWithoutLaser()
    {
        yield return new WaitForFixedUpdate();
        
        Debug.Log("Expected: " + laserPointerRecieverScript.hitColour);
        Debug.Log("Actual  : " + lprMeshRender.material.color);
        
        // The colour should not have changed yet
        Assert.AreEqual(laserPointerRecieverScript.defaultColour.r, lprMeshRender.material.color.r);
        Assert.AreEqual(laserPointerRecieverScript.defaultColour.g, lprMeshRender.material.color.g);
        Assert.AreEqual(laserPointerRecieverScript.defaultColour.b, lprMeshRender.material.color.b);
        Assert.AreEqual(laserPointerRecieverScript.defaultColour.a, lprMeshRender.material.color.a);
    }
    
    [UnityTest, Order(2)]
    public IEnumerator LaserPointerTestWithLaser()
    {
        laserPointerScript.turnOnLaser = true;

        yield return new WaitForFixedUpdate();
        
        Debug.Log("Expected: " + laserPointerRecieverScript.hitColour);
        Debug.Log("Actual  : " + lprMeshRender.material.color);

        // The colour should not be the default one
        Assert.AreEqual(laserPointerRecieverScript.hitColour.r, lprMeshRender.material.color.r);
        Assert.AreEqual(laserPointerRecieverScript.hitColour.g, lprMeshRender.material.color.g);
        Assert.AreEqual(laserPointerRecieverScript.hitColour.b, lprMeshRender.material.color.b);
        Assert.AreEqual(laserPointerRecieverScript.hitColour.a, lprMeshRender.material.color.a);
    }
}
