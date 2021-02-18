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
    public Camera pointer;
    public SteamVR_Input_Sources targetSource;
    public SteamVR_Action_Boolean clickAction = SteamVR_Input.GetBooleanAction("PullObject");

    private GameObject currentObject;
    private PointerEventData pointerData;

    protected override void Start()
    {
        // Populate pointer data with pointer position
        pointerData = new PointerEventData(eventSystem)
        {
            position = new Vector2(pointer.pixelWidth / 2, pointer.pixelHeight / 2)
        };
    }

    public override void Process()
    {
        // Get Raycast data
        eventSystem.RaycastAll(pointerData, m_RaycastResultCache);
        pointerData.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);

        // Execute hover events
        HandlePointerExitAndEnter(pointerData, pointerData.pointerCurrentRaycast.gameObject);
        
        // Execute drag events
        ExecuteEvents.Execute(pointerData.pointerDrag, pointerData, ExecuteEvents.dragHandler);

        // Trigger Press
        if (clickAction.GetStateDown(targetSource))
            ProcessPress();

        // Trigger Release
        if (clickAction.GetStateUp(targetSource))
            ProcessRelease();
    }

    private void ProcessPress()
    {
        // Set raycast
        pointerData.pointerPressRaycast = pointerData.pointerCurrentRaycast;

        // Get click and drag handlers
        pointerData.pointerPress =
            ExecuteEvents.GetEventHandler<IPointerClickHandler>(pointerData.pointerPressRaycast.gameObject);
        pointerData.pointerDrag =
            ExecuteEvents.GetEventHandler<IDragHandler>(pointerData.pointerPressRaycast.gameObject);

        // Execute pointer down and start drag events
        ExecuteEvents.Execute(pointerData.pointerPress, pointerData, ExecuteEvents.pointerDownHandler);
        ExecuteEvents.Execute(pointerData.pointerDrag, pointerData, ExecuteEvents.beginDragHandler);
    }

    private void ProcessRelease()
    {
        // Get pointer click handler
        GameObject pointerRelease = ExecuteEvents.GetEventHandler<IPointerClickHandler>(pointerData.pointerCurrentRaycast.gameObject);

        // If the same as down handler, execute click
        if (pointerData.pointerPress == pointerRelease)
            ExecuteEvents.Execute(pointerData.pointerPress, pointerData, ExecuteEvents.pointerClickHandler);

        // Execute pointer up and end drag events
        ExecuteEvents.Execute(pointerData.pointerPress, pointerData, ExecuteEvents.pointerUpHandler);
        ExecuteEvents.Execute(pointerData.pointerDrag, pointerData, ExecuteEvents.endDragHandler);

        // Clean up data
        pointerData.pointerPress = null;
        pointerData.pointerDrag = null;
        pointerData.pointerCurrentRaycast.Clear();
    }

    public PointerEventData GetData()
    {
        return pointerData;
    }
}
