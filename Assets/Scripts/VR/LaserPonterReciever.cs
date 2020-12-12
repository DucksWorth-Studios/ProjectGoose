
using UnityEngine;

public class LaserPonterReciever : MonoBehaviour
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

    public void Click(Transform handLocation)
    {
        meshRenderer.material.color = clickColour;
        
        transform.position = handLocation.position;
        
        RayExit();
    }
}
