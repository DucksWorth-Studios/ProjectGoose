using UnityEngine;

public class LaserPonterReciever : MonoBehaviour
{
    private Color defaultColour = Color.white;
    public Color hitColour = Color.red;
    public Color clickColour = Color.green;

    private MeshRenderer meshRenderer;

    public Material defaultMaterial;
    public Material outlineMaterial;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        defaultMaterial = meshRenderer.materials[0];
        defaultColour = meshRenderer.material.color;

        outlineMaterial = (Material)Resources.Load("Materials/Outline", typeof(Material));

        if (!outlineMaterial)
            Debug.LogError("Failed to load Outline material from resources folder");
        else
            outlineMaterial.mainTexture = defaultMaterial.mainTexture;
    }

    public void HitByRay()
    {
        meshRenderer.material = outlineMaterial;
    }
    
    public void RayExit()
    {
        meshRenderer.material = defaultMaterial;
        meshRenderer.material.color = defaultColour;
    }

    public void Click()
    {
        meshRenderer.material.color = clickColour;
    }
}