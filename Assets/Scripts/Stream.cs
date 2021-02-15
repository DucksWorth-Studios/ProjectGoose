using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stream : MonoBehaviour
{
    private LineRenderer lineRenderer = null;
    private ParticleSystem splashParticle = null;

    private Coroutine pourRoutine = null;
    private Vector3 targetPosition = Vector3.zero;

    //Used for color mixing
    private GameObject gameObjectCollide = null;
    private CompositionManager compositionManager = null;

    private void Awake()
    {
        //chemicalColor = GetComponent<Material>().color;

        lineRenderer = GetComponent<LineRenderer>();
        splashParticle = GetComponentInChildren<ParticleSystem>();
    }
    /// <summary>
    /// Author: Tomas
    /// Use this to reference the composition
    /// </summary>
    /// <param name="compositionManager">The objects compositionManager</param>
    public void setCompositionManager(CompositionManager compositionManager)
    {
        this.compositionManager = compositionManager;
        //Set the linerenderer color
        lineRenderer.material.color = compositionManager.currentColor;
    }

    private void Start()
    {
        //Set begining and end of line renderer
        MoveToPosition(0, transform.position);

        MoveToPosition(1, transform.position);
    }

    /// <summary>
    /// Begins and initializes the Stream.
    /// </summary>
    public void Begin()
    {
        //Start the routines for pour and particle
        StartCoroutine(UpdateParticle());
        pourRoutine = StartCoroutine(BeginPour());
    }

    /// <summary>
    /// While the stream is active calculate the distance to the object below and slowly extend the stream down till it collides
    /// </summary>
    /// <returns></returns>
    private IEnumerator BeginPour()
    {
        //While object is active
        while (gameObject.activeSelf)
        {
            //find object below
            targetPosition = FindEndpoint();
            //Begining of line to the origin point
            MoveToPosition(0, transform.position);

            //End of line animate to object bellow
            AnimateToPosition(1, targetPosition);

            yield return null;
        }
    }

    /// <summary>
    /// Stop the pouring and begin the ending routine
    /// </summary>
    public void End()
    {
        //End pouring
        StopCoroutine(pourRoutine);
        //begin the end routine
        pourRoutine = StartCoroutine(EndPour());
    }

    /// <summary>
    /// Ends stream coming from the tip of the object. the begining of stream will slowly fall down to end point creating a drop effect
    /// </summary>
    private IEnumerator EndPour()
    {
        //while we have not reached the end position
        while (!HasReachedPosition(0, targetPosition))
        {
            //Move start down slowly away from origin
            AnimateToPosition(0, targetPosition);
            AnimateToPosition(0, targetPosition);
            yield return null;
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// Find the endpoint directly below the origin.
    /// </summary>
    /// <returns></returns>
    private Vector3 FindEndpoint()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        //Generate Ray
        Physics.Raycast(ray, out hit, 2.0f);
        //set the object
        if (hit.collider.gameObject.tag == AppData.chemicalTag)
        {
            gameObjectCollide = hit.collider.gameObject;
        }
        else if(hit.collider.gameObject.tag == AppData.playerTag)
        {
            compositionManager.IsPerfectCompositionDrunk();
        }

        //if it hits valid collider set it as end point
        Vector3 endPoint = hit.collider ? hit.point : ray.GetPoint(2.0f);

        return endPoint;
    }

    /// <summary>
    /// Will be used to update the stream via the linerenderer
    /// </summary>
    /// <param name="index">The start or end of the line renderer</param>
    /// <param name="targetPosition"> Where to move to</param>
    private void MoveToPosition(int index, Vector3 targetPosition)
    {
        //Take in the index of the line and move to a specified position
        lineRenderer.SetPosition(index, targetPosition);
    }

    /// <summary>
    /// Will be used to update the animation via the linerenderer
    /// </summary>
    /// <param name="index">The start or end of the line renderer</param>
    /// <param name="targetPosition"> Where to animate to</param>
    private void AnimateToPosition(int index, Vector3 targetPosition)
    {
        Vector3 currentPoint = lineRenderer.GetPosition(index);
        Vector3 newPosition = Vector3.MoveTowards(currentPoint, targetPosition, Time.deltaTime * 1.75f);
        lineRenderer.SetPosition(index, newPosition);
    }
    /// <summary>
    /// Has the start or end of the stream reached the target
    /// </summary>
    /// <param name="index">>The start or end of the line renderer</param>
    /// <param name="targetPosition"> The end position</param>
    /// <returns>True or False</returns>
    private bool HasReachedPosition(int index, Vector3 targetPosition)
    {
        //Check has it reached the target
        Vector3 currentPosition = lineRenderer.GetPosition(index);

        return currentPosition == targetPosition;
    }

    /// <summary>
    /// If the end of the linerenderer is at the target position create a small splash effect
    /// </summary>
    private IEnumerator UpdateParticle()
    {
        while (gameObject.activeSelf)
        {
            splashParticle.gameObject.transform.position = targetPosition;
            //is the object hitting the target?
            bool isHitting = HasReachedPosition(1, targetPosition);


            //activate the splash effect
            splashParticle.gameObject.SetActive(isHitting);
            //Change color when we hit
            if (gameObjectCollide != null  && isHitting)
            {
                compositionManager.callColorChange(gameObjectCollide);
                //gameObjectCollide.GetComponent<ColorChange>().switchColour(chemicalColor);
            }
            yield return null;
        }

    }
}
