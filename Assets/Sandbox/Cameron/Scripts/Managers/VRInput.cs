using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRInput : BaseInput
{
    public Camera eventCamera;

    public SteamVR_Action_Boolean pullObject = SteamVR_Input.GetBooleanAction("PullObject");

    protected  override void Awake()
    {
        GetComponent<BaseInputModule>().inputOverride = this;
    }

    public override bool GetMouseButtonDown(int button)
    {
        return pullObject.stateDown;
    }

    public override bool GetMouseButtonUp(int button)
    {
        return pullObject.stateUp;
    }

    public override bool GetMouseButton(int button)
    {
        return pullObject.state;
    }

    public override Vector2 mousePosition
    {
        get
        {
            return new Vector2(eventCamera.pixelWidth / 2, eventCamera.pixelHeight / 2);
        }
    }
}