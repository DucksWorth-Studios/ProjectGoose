using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class VRUIRaycaster : GraphicRaycaster, IPointerEnterHandler
{
    [Tooltip("A world space pointer for this canvas")]
    public GameObject pointer;
    
    private Canvas canvas;
    private List<RaycastHit> raycastResults = new List<RaycastHit>();

    public override Camera eventCamera
    {
        get
        {
            return canvas.worldCamera;
        }
    }
    
    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
    {
        Ray ray = eventData.GetRay();
        
        if (canvas == null)
            return;

        float hitDistance = float.MaxValue;

        // if (checkForBlocking && blockingObjects != BlockingObjects.None)
        // {
            float dist = eventCamera.farClipPlane;

            if (blockingObjects == BlockingObjects.ThreeD || blockingObjects == BlockingObjects.All)
            {
                var hits = Physics.RaycastAll(ray, dist, m_BlockingMask);

                if (hits.Length > 0 && hits[0].distance < hitDistance)
                {
                    hitDistance = hits[0].distance;
                }
            }

            if (blockingObjects == BlockingObjects.TwoD || blockingObjects == BlockingObjects.All)
            {
                var hits = Physics2D.GetRayIntersectionAll(ray, dist, m_BlockingMask);

                if (hits.Length > 0 && hits[0].fraction * dist < hitDistance)
                {
                    hitDistance = hits[0].fraction * dist;
                }
            }
        // }

        raycastResults.Clear();

        GraphicRaycast(canvas, ray, raycastResults);

        for (var index = 0; index < raycastResults.Count; index++)
        {
            var go = raycastResults[index].graphic.gameObject;
            bool appendGraphic = true;

            if (ignoreReversedGraphics)
            {
                // If we have a camera compare the direction against the cameras forward.
                var cameraFoward = ray.direction;
                var dir = go.transform.rotation * Vector3.forward;
                appendGraphic = Vector3.Dot(cameraFoward, dir) > 0;
            }

            // Ignore points behind us (can happen with a canvas pointer)
            if (eventCamera.transform.InverseTransformPoint(raycastResults[index].worldPos).z <= 0)
            {
                appendGraphic = false;
            }

            if (appendGraphic)
            {
                float distance = Vector3.Distance(ray.origin, raycastResults[index].worldPos);

                if (distance >= hitDistance)
                {
                    continue;
                }

                var castResult = new RaycastResult
                {
                    gameObject = go,
                    module = this,
                    distance = distance,
                    index = resultAppendList.Count,
                    depth = raycastResults[index].graphic.depth,

                    worldPosition = raycastResults[index].worldPos
                };
                resultAppendList.Add(castResult);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
