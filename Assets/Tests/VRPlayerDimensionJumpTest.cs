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

    private GameObject point1;
    private GameObject point2;
    private GameObject point3;
    private GameObject point4;
    
    private GameObject player;
    private VRPlayerDimensionJump jumpScript;

    [OneTimeSetUp]
    public void Setup()
    {
        MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/_EventManager"));
        
        GameObject plane = Resources.Load<GameObject>("Prefabs/Plane");
        plane1 = MonoBehaviour.Instantiate(plane);
        plane2 = MonoBehaviour.Instantiate(plane);
        player = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/VR/VRPlayer"));

        // Create empty game objects to be used as teleportation points
        point1 = new GameObject();
        point1.transform.position = new Vector3(15, 100, 15);
        
        point2 = new GameObject();
        point2.transform.position = new Vector3(-15, 100, 15);
        
        point3 = new GameObject();
        point3.transform.position = new Vector3(15, 100, -15);
        
        point4 = new GameObject();
        point4.transform.position = new Vector3(-15, 100, -15);

        plane2.transform.position = new Vector3(0, 100, 0);
        jumpScript = player.GetComponent<VRPlayerDimensionJump>();
        
        // Add the teleport points to the jump script after it has been initialised
        GameObject[] points = {point1, point2, point3, point4};
        jumpScript.teleportPoints = points;
    }

    [UnityTest, Order(1)]
    public IEnumerator DimensionJumpTestJumpUp()
    {
        jumpScript.DimensionJump();

        // A UnityTest must return an IEnumerator
        yield return new WaitForSeconds(0.1f);

        Vector3 playerPosition = player.transform.position;
        
        Assert.True(jumpScript.IsInUpSideDown);
        Assert.AreEqual(playerPosition.y, plane2.transform.position.y);
        // The player x and y position should be randomised between 15 and -15
        Assert.AreNotEqual(playerPosition.x, 0);
        Assert.AreNotEqual(playerPosition.z, 0);
    }
    
    [UnityTest, Order(2)]
    public IEnumerator DimensionJumpTestJumpDown()
    {
        Assert.True(jumpScript.IsInUpSideDown);
        jumpScript.DimensionJump();

        // A UnityTest must return an IEnumerator
        yield return new WaitForSeconds(0.1f);
        
        Vector3 playerPosition = player.transform.position;
        
        Assert.False(jumpScript.IsInUpSideDown);
        Assert.AreEqual(playerPosition.y, plane1.transform.position.y);
        // The player should now be back at the position they teleported from
        Assert.AreEqual(playerPosition.x, 0);
        Assert.AreEqual(playerPosition.z, 0);
    }
}
