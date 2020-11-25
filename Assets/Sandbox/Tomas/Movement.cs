using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Variables
    public GameObject cube;
    private void Awake()
    {
        
    }
    void Update()
    {
        cube.transform.position += new Vector3(0f, 0f, 0.002f);
    }
}

