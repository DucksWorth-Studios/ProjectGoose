using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Author: Cameron Scholes
/// References: https://docs.unity3d.com/ScriptReference/Shader.WarmupAllShaders.html
///             https://docs.unity3d.com/ScriptReference/ShaderVariantCollection.html
///             https://gamedevbeginner.com/how-to-load-a-new-scene-in-unity-with-a-loading-screen/
/// </summary>

public class LoadingManager : MonoBehaviour
{
    [Header("Scene Loading")] 
    public bool loadScene = true;
    public string sceneToLoad;

    private AsyncOperation loadingOperation;
    private bool isLoadingScene;

    [Header("Loading UI")] 
    public Slider progressBar;
    public GameObject vrPlayer;

    [Header("Shader WarmUp")]
    public ShaderVariantCollection shaderVariantCollection;
    
    void Start()
    {
        SceneManager.sceneLoaded += DestroyLoadPlayer;
        shaderVariantCollection.WarmUp();
    }

    void Update()
    {
        // This ensures all shaders are warmed up before level loading takes over the scene transition
        if (loadScene && shaderVariantCollection.isWarmedUp && !isLoadingScene)
        {
            loadingOperation = SceneManager.LoadSceneAsync(sceneToLoad);
            isLoadingScene = true;
        }

        if (isLoadingScene)
        {
            // Loading progress is only measured up to 90%
            progressBar.value = Mathf.Clamp01(loadingOperation.progress / 0.9f);
        }
    }
    
    // This will destroy the VR Player from the loading scene once the level is loaded
    private void DestroyLoadPlayer(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.isLoaded)
            Destroy(vrPlayer);
    }
}
