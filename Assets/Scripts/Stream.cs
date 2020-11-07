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
    /// Ends stream coming from the tip of the object. the begining of stream will slowly fall down to end point creating a drop effect
    /// </summary>
    public void End()
    {

    }

    private IEnumerator EndPour()
    {
        return null;
    }
    private Vector3 FindEndpoint()
    {
        return Vector3.zero;
    }

    private void MoveToPosition(int index, Vector3 targetPosition)
    {
       
    }

    private void AnimateToPosition(int index, Vector3 targetPosition)
    {

    }

    private bool HasReachedPosition(int index, Vector3 targetPosition)
    {
        return false;
    }

    private IEnumerator UpdateParticle()
    {
        return null;
    }
}
