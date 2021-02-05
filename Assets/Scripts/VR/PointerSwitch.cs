using UnityEngine;

public enum PointerState {
    PhysicsPointer,
    CanvasPointer,
    Disabled
}

public class PointerSwitch : MonoBehaviour
{
    public PointerState pointerState;
    public GameObject canvasPointer;

    private PointerState lastState;
    private LineRenderer physicsLineRender;
    private LaserPointer physicsLaserPointer;
    
    void Start()
    {
        lastState = pointerState;
        physicsLineRender = GetComponent<LineRenderer>();
        physicsLaserPointer = GetComponent<LaserPointer>();
        UpdateState();
    }
    
    void Update()
    {
        if (pointerState != lastState)
        {
            lastState = pointerState;
            UpdateState();
        }
    }

    private void UpdateState()
    {
        canvasPointer.SetActive(pointerState == PointerState.CanvasPointer);
        physicsLineRender.enabled = pointerState == PointerState.PhysicsPointer;
        physicsLaserPointer.enabled = pointerState == PointerState.PhysicsPointer;
    }
}
