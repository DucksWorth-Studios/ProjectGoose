using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

/// <summary>
/// Author: Cameron Scholes & VR With Andrew
///
/// Input Module that uses graphics raycasting to interact with a canvas
/// 
/// Resource: https://www.youtube.com/watch?v=vNqHRD4sqPc
/// </summary>

public class VRCanvasInput : BaseInputModule
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
        // Set Raycast
        data.pointerPressRaycast = data.pointerCurrentRaycast;
        
        // Check for object hit, get down handler, call
        GameObject newPointerPress =
            ExecuteEvents.ExecuteHierarchy(currentObject, data, ExecuteEvents.pointerDownHandler);

        // If no down handler, try and get click handler
        if (newPointerPress == null)
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);

        // Set data
        data.pressPosition = data.position;
        data.pointerPress = newPointerPress;
        data.rawPointerPress = currentObject;
    }

    private void ProcessRelease(PointerEventData data)
    {
        // Execute pointer up
        ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

        // Check for click handler
        GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);

        // Check for pointer match
        if (data.pointerPress == pointerUpHandler)
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);

        // Clear selected gameObject
        eventSystem.SetSelectedGameObject(null);

        // Reset Data
        data.pressPosition = Vector2.zero;
        data.pointerPress = null;
        data.rawPointerPress = null;
    }
}
