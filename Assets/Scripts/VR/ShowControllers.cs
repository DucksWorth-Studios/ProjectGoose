using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// Author: Cameron Scholes
/// A script to show the player's VR controller with the hands model
/// </summary>
public class ShowControllers : MonoBehaviour
{
    [Tooltip("If enabled, the players controller will be displayed with their hands.")]
    public bool showController;

    /// <summary>
    /// Every frame, show controllers and update hand pose
    /// </summary>
    void Update()
    {
        // By default controllers aren't shown so this update is only required if you want to show controllers
        if (!showController) return;

        // TODO: Is there a more permanent way of doing this?
        foreach (var hand in Player.instance.hands)
        {
            hand.ShowController();
            hand.SetSkeletonRangeOfMotion(Valve.VR.EVRSkeletalMotionRange.WithController);
        }
    }
}
