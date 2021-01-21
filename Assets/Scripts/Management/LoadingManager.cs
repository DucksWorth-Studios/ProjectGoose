using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Cameron Scholes
/// References: https://docs.unity3d.com/ScriptReference/Shader.WarmupAllShaders.html
///             https://docs.unity3d.com/ScriptReference/ShaderVariantCollection.html
/// </summary>

public class LoadingManager : MonoBehaviour
{
    ShaderVariantCollection shaderVariantCollection;
    
    void Start()
    {
        shaderVariantCollection.WarmUp();
    }
}
