
using UnityEngine;

public class LaserPointerReciever : MonoBehaviour
{
    public Color defaultColour = Color.white;
    public Color hitColour = Color.red;
    public Color clickColour = Color.green;

    private MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.color = defaultColour;
    }

    public void HitByRay()
    {
        meshRenderer.material.color = hitColour;
    }
    
    public void RayExit()
    {
        meshRenderer.material.color = defaultColour;
    }

    public void Click()
    {
        meshRenderer.material.color = clickColour;
    }
}
