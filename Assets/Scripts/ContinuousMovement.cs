using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ContinuousMovement : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;
    public float speed = 1;
    private CharacterController _characterController;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Prevent locomotion interfering with teleportation
        if (input.axis.magnitude > 0.1f)
        {
            Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));
            // First line is movement based on where the headset is. ProjectOnPlane ensures that all movement is horizontal
            // Second line is adding gravity
            _characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) 
                                      - new Vector3(0, 9.81f, 0) * Time.deltaTime);
        }
        else
        {
            Vector3 direction = Player.instance.hmdTransform.localPosition;
            _characterController.center = new Vector3(direction.x, _characterController.center.y, direction.z);
        }
    }
}
