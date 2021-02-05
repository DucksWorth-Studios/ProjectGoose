using UnityEngine;

/// <summary>
/// Author: Cameron Scholes
/// Add script to objects that should be destroyed when changing scenes
/// </summary>
public class DontDestory : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
