using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Cameron Scholes
/// The script will spawn the smoke effect and slowly close in on the player
/// </summary>
public class SmokeRing : MonoBehaviour
{
    public GameObject smokePrefab;
    
    [Tooltip("The number of fog effect prefabs to spawn")]
    public int spawnLimit = 5;
    
    [Tooltip("The initial size of the spawn radius")]
    public float spawnRadius = 5;
    
    [Tooltip("The limit for how small the final fog radius should be")]
    public float spawnRadiusLimit = 1;
    
    [Tooltip("Time is spawn radius, value is spawned effects.")]
    public AnimationCurve spawnCurve = AnimationCurve.Linear(0, 10, 20, 5);
    
    [Tooltip("The speed the smoke should move at")]
    public float moveSpeed = 5;

    private int initialSpawnLimit;
    private float initialSpawnRadius;
    private List<GameObject> spawned;
    private List<VFXManager> spawnedVFX;
    private int lastSpwanCount;

    void Start()
    {
        initialSpawnLimit = spawnLimit;
        initialSpawnRadius = spawnRadius;

        if (!SceneManager.GetActiveScene().name.Equals("FogTesting"))
        {
            EventManager.instance.OnTimeJump += UpdateSmokeEffect;
            EventManager.instance.OnLoseGame += Destroy;
        } 
        else
            UpdateSmokeEffect();
    }

    private void UpdateSmokeEffect()
    {
        if (spawned != null && spawned.Count > 0)
        {
            // Player has already jumped to the future and has now jumped back to the past
            Destroy();
            StopCoroutine("ShrinkFogArea");

            spawnLimit = initialSpawnLimit;
            spawnRadius = initialSpawnRadius;
        }
        else
        {
            // Player has just jumped to the future
            Reset();
            StartCoroutine("ShrinkFogArea");
        }
    }

    private IEnumerator ShrinkFogArea()
    {
        while (spawnRadius > spawnRadiusLimit)
        {
            spawnRadius -= 1;

            int difference = lastSpwanCount - Mathf.CeilToInt(spawnCurve.Evaluate(spawnRadius));
            spawnLimit -= difference;

            for (int i = 0; i < difference; i++)
            {
                Destroy(spawned[0]);
                spawned.RemoveAt(0);
                spawnedVFX.RemoveAt(0);
            }

            MoveObjects();
            yield return new WaitForSeconds(1);
        }
    }
    void Destroy()
    {
        foreach (GameObject ob in spawned)
            Destroy(ob);

        spawned = null;
    }
    
    void Reset()
    {
        spawned = new List<GameObject>();
        spawnedVFX = new List<VFXManager>();
        SpawnObjects();
    }
    
    private void SpawnObjects()
    {
        lastSpwanCount = spawnLimit;
        
        for (int i = 0; i < spawnLimit; i++)
        {
            Vector3 localPosition = transform.position;
            
            float theta = i * 2 * Mathf.PI / spawnLimit;
            float x = Mathf.Sin(theta)*spawnRadius + localPosition.x;
            float z = Mathf.Cos(theta)*spawnRadius + localPosition.z;
  
            GameObject ob = Instantiate(smokePrefab, transform, true);
            Vector3 newPosition = new Vector3(x, localPosition.y, z);
            ob.transform.position = newPosition;
            
            VFXManager vfx = ob.GetComponent<VFXManager>();
            vfx.target = newPosition;
            vfx.moveSpeed = moveSpeed;
            
            ob.name = "Fog-" + i;
            spawned.Add(ob);
            spawnedVFX.Add(vfx);
        }
    }
    
    private void MoveObjects()
    {
        lastSpwanCount = spawnLimit;
        
        for (int i = 0; i < spawnLimit; i++)
        {
            Vector3 localPosition = transform.position;
            
            float theta = i * 2 * Mathf.PI / spawnLimit;
            float x = Mathf.Sin(theta)*spawnRadius + localPosition.x;
            float z = Mathf.Cos(theta)*spawnRadius + localPosition.z;

            spawnedVFX[i].target = new Vector3(x, localPosition.y, z);
        }
    }
}
