using UnityEngine;


public class PointerSwitch : MonoBehaviour
{
    public PointerState pointerState;
    public GameObject canvasPointer;
    public GameObject notRussels;

    private PointerState lastState;
    private LineRenderer physicsLineRender;
    private LaserPointer physicsLaserPointer;
    
    void Start()
    {
        lastState = pointerState;
        physicsLineRender = GetComponent<LineRenderer>();
        physicsLaserPointer = GetComponent<LaserPointer>();
        
        EventManager.instance.OnSetPhysicsPointer += SetPhysicsPointer;
        EventManager.instance.OnSetUIPointer += SetUIPointer;
        EventManager.instance.OnDisablePointer += DisablePointerState;
        
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
        notRussels.SetActive(pointerState == PointerState.NotRussels);
    }
    
    private void SetPhysicsPointer()
    {
        pointerState = PointerState.PhysicsPointer;
    }
    
    private void SetNotRussels()
    {
        pointerState = PointerState.NotRussels;
    }

    private void SetUIPointer()
    {
        pointerState = PointerState.CanvasPointer;
    }

    private void DisablePointerState()
    {
        pointerState = PointerState.Disabled;
    }
}
