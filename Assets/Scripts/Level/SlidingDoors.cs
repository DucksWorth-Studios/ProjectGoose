using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoors : MonoBehaviour
{
    private Collider collisionBox;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        var colliders = GetComponentsInChildren<BoxCollider>();

        collisionBox = colliders[colliders.Length - 1];
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
