using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// Author: Cameron Scholes
/// Tests the player dimension jump ability
/// </summary>
public class VRPlayerDimensionJumpTest
{
    private GameObject plane1;
    private GameObject plane2;
    
    private GameObject player;
    private VRPlayerDimensionJump jumpScript;

    [OneTimeSetUp]
    public void Setup()
    {
        GameObject plane = Resources.Load<GameObject>("Prefabs/Plane");
        plane1 = MonoBehaviour.Instantiate(plane);
        plane2 = MonoBehaviour.Instantiate(plane);
        player = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/VR/VRPlayer"));
        
        plane2.transform.position = new Vector3(0, 100, 0);
        jumpScript = player.GetComponent<VRPlayerDimensionJump>();
    }

    [UnityTest, Order(1)]
    public IEnumerator DimensionJumpTestJumpUp()
    {
        jumpScript.DimensionJump();

        // A UnityTest must return an IEnumerator
        yield return new WaitForSeconds(0.1f);
        
        Assert.True(jumpScript.IsInUpSideDown);
        Assert.AreEqual(player.transform.position.y, plane2.transform.position.y);
    }
    
    [UnityTest, Order(2)]
    public IEnumerator DimensionJumpTestJumpDown()
    {
        Assert.True(jumpScript.IsInUpSideDown);
        jumpScript.DimensionJump();

        // A UnityTest must return an IEnumerator
        yield return new WaitForSeconds(0.1f);
        
        Assert.False(jumpScript.IsInUpSideDown);
        Assert.AreEqual(player.transform.position.y, plane1.transform.position.y);
    }
}
