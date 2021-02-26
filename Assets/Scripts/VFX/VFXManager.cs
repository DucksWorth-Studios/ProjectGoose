using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [HideInInspector]
    public Vector3 target;
    public static float moveSpeed = 2.5f;

    void Update()
    {
        if (Vector3.Distance(transform.position, target) < 0.001f)
            return;
        
        float step =  moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }
}
