using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class VRItemDimensionJumpTest
{
    private GameObject interactable;
    private VRItemDimensionJump jumpScript;

    [OneTimeSetUp]
    public void Setup()
    {
        MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/_EventManager"));
        
        interactable = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/VR/Interactable"));
        jumpScript = interactable.GetComponent<VRItemDimensionJump>();
    }

    [UnityTest, Order(1)]
    public IEnumerator JumpWithoutHoldingItem()
    {
        EventManager.instance.TimeJump();

        // A UnityTest must return an IEnumerator
        yield return new WaitForSeconds(0.1f);
        
        Assert.False(jumpScript.inFuture);
        
        Assert.True(jumpScript.normalObject.activeSelf);
        Assert.False(jumpScript.agedObject.activeSelf);
    }
    
    [UnityTest, Order(2)]
    public IEnumerator JumpToFutureWithItem()
    {
        jumpScript.IsAttached = true;
        EventManager.instance.TimeJump();

        // A UnityTest must return an IEnumerator
        yield return new WaitForSeconds(0.1f);
        
        Assert.True(jumpScript.inFuture);
        
        Assert.False(jumpScript.normalObject.activeSelf);
        Assert.True(jumpScript.agedObject.activeSelf);
    }
    
    [UnityTest, Order(3)]
    public IEnumerator JumpFromFutureWithoutHoldingItem()
    {
        jumpScript.IsAttached = false;
        EventManager.instance.TimeJump();

        // A UnityTest must return an IEnumerator
        yield return new WaitForSeconds(0.1f);
        
        Assert.True(jumpScript.inFuture);
        
        Assert.False(jumpScript.normalObject.activeSelf);
        Assert.True(jumpScript.agedObject.activeSelf);
    }
    
    [UnityTest, Order(4)]
    public IEnumerator JumpToPresentWithItem()
    {
        jumpScript.IsAttached = true;
        EventManager.instance.TimeJump();

        // A UnityTest must return an IEnumerator
        yield return new WaitForSeconds(0.1f);
        
        Assert.False(jumpScript.inFuture);
        
        Assert.True(jumpScript.normalObject.activeSelf);
        Assert.False(jumpScript.agedObject.activeSelf);
    }
}
