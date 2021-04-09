using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Animator))]
public class SphereRobotController : MonoBehaviour
{
    private Animator animator;

    private Throwable throwable;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        throwable = GetComponent<Throwable>();
        rb = GetComponent<Rigidbody>();

        throwable.onPickUp.AddListener(PickedUp);
        throwable.onDetachFromHand.AddListener(Dropped);
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y < -5)
            rb.angularVelocity = Vector3.zero;
    }

    private void PickedUp() => animator.SetBool("inHand", true);

    private void Dropped() => animator.SetBool("inHand", false);
}
