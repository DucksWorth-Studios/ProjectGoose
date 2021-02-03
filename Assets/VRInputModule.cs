using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRInputModule : BaseInputModule
{
    public Camera camera;
    public SteamVR_Input_Sources targetSource;
    public SteamVR_Action_Boolean clickAction;

    private GameObject currentObject;
    private PointerEventData pointerData;

    protected override void Awake()
    {
        base.Awake();
        
        pointerData = new PointerEventData(eventSystem);
    }

    public override void Process()
    {
        // Reset Data, Set Camera
        pointerData.Reset();
        pointerData.position = new Vector2(camera.pixelWidth / 2, camera.pixelHeight / 2);

        // Raycast
        eventSystem.RaycastAll(pointerData, m_RaycastResultCache);
        pointerData.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        currentObject = pointerData.pointerCurrentRaycast.gameObject;

        // Clear
        m_RaycastResultCache.Clear();

        // Hover
        HandlePointerExitAndEnter(pointerData, currentObject);

        // Press
        if (clickAction.GetStateDown(targetSource))
            ProcessPress(pointerData);

        // Release
        if (clickAction.GetStateUp(targetSource))
            ProcessRelease(pointerData);
    }

    public PointerEventData GetData()
    {
        return pointerData;
    }

    private void ProcessPress(PointerEventData data)
    {
        throw new System.NotImplementedException();
    }

    private void ProcessRelease(PointerEventData data)
    {
        throw new System.NotImplementedException();
    }
}
