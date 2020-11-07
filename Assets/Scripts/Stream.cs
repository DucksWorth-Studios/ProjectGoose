using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stream : MonoBehaviour
{
    private LineRenderer lineRenderer = null;
    private ParticleSystem splashParticle = null;

    private Coroutine pourRoutine = null;
    private Vector3 targetPosition = Vector3.zero;

    private void Awake()
    {

    }

    private void Start()
    {

    }
    /// <summary>
    /// Begins and initializes the Stream.
    /// </summary>
    public void Begin()
    {

    }
    /// <summary>
    /// While the stream is active calculate the distance to the object below and slowly extend the stream down till it collides
    /// </summary>
    /// <returns></returns>
    private IEnumerator BeginPour()
    {
        return null;
    }
    /// <summary>
    /// Stop the pouring and begin the ending routine
    /// </summary>
    public void End()
    {

    }

    /// <summary>
    /// Ends stream coming from the tip of the object. the begining of stream will slowly fall down to end point creating a drop effect
    /// </summary>
    private IEnumerator EndPour()
    {
        return null;
    }

    /// <summary>
    /// Find the endpoint directly below the origin.
    /// </summary>
    /// <returns></returns>
    private Vector3 FindEndpoint()
    {
        return Vector3.zero;
    }

    /// <summary>
    /// Will be used to update the stream via the linerenderer
    /// </summary>
    /// <param name="index">The start or end of the line renderer</param>
    /// <param name="targetPosition"> Where to move to</param>
    private void MoveToPosition(int index, Vector3 targetPosition)
    {
       
    }

    /// <summary>
    /// Will be used to update the animation via the linerenderer
    /// </summary>
    /// <param name="index">The start or end of the line renderer</param>
    /// <param name="targetPosition"> Where to animate to</param>
    private void AnimateToPosition(int index, Vector3 targetPosition)
    {

    }
    /// <summary>
    /// Has the start or end of the stream reached the target
    /// </summary>
    /// <param name="index">>The start or end of the line renderer</param>
    /// <param name="targetPosition"> The end position</param>
    /// <returns>True or False</returns>
    private bool HasReachedPosition(int index, Vector3 targetPosition)
    {
        return false;
    }

    /// <summary>
    /// If the end of the linerenderer is at the target position create a small splash effect
    /// </summary>
    private IEnumerator UpdateParticle()
    {
        return null;
    }
}
