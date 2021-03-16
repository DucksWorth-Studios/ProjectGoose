using UnityEngine;

public class VFXManager : MonoBehaviour
{
    private Vector3 target;
    public static float moveSpeed = 5;

    public Vector3 Target
    {
        get => target;
        set
        {
            target = value;
            // Debug.Log(gameObject.name + " Target: " + target);
        }
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target) < 0.001f)
            return;
        
        float step =  moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }
}
