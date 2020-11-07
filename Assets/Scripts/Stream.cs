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

    public void Begin()
    {

    }

    private IEnumerator BeginPour()
    {
        return null;
    }

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
