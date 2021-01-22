using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingTest : MonoBehaviour
{
    [Header("Shader WarmUp")]
    public ShaderVariantCollection shaderVariantCollection;
    
    void Start()
    {
        Debug.Log("Is shader collection warmed up: " + shaderVariantCollection.isWarmedUp);
    }
}
