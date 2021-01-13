using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    
    [Tooltip("The limit for how small the fianll fog radius should be")]
    public float spawnRadiusLimit = 1;
    
    [Tooltip("Time is spawn radius, value is spawned effects.")]
    public AnimationCurve spawnCurve = AnimationCurve.Linear(0, 10, 20, 5);

    private int initialSpawnLimit;
    private float initialSpawnRadius;
    private List<GameObject> spawned;
    private int lastSpwanCount;

    void Start()
    {
        initialSpawnLimit = spawnLimit;
        initialSpawnRadius = spawnRadius;
        EventManager.instance.OnTimeJump += UpdateSmokeEffect;
    }

    private void UpdateSmokeEffect()
    {
        if (spawned != null && spawned.Count > 0)
        {
            // Player has already jumped to the future and has now jumped back to the past
            Destroy();
            CancelInvoke("ShrinkFogArea");

            spawnLimit = initialSpawnLimit;
            spawnRadius = initialSpawnRadius;
        }
        else
        {
            // Player has just jumped to the future
            Reset();
            InvokeRepeating("ShrinkFogArea", 1, 1);
        }
    }

    void ShrinkFogArea()
    {
        if (spawnRadius > spawnRadiusLimit)
        {
            spawnRadius -= 1;

            int difference = lastSpwanCount - Mathf.CeilToInt(spawnCurve.Evaluate(spawnRadius));
            spawnLimit -= difference;

            for (int i = 0; i < difference; i++)
            {
                // Debug.Log("Removed");
                Destroy(spawned[0]);
                spawned.RemoveAt(0);
            }

            MoveObjects();
        } 
        else
        {
            CancelInvoke("ShrinkFogArea");
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
            ob.transform.position = new Vector3(x, localPosition.y, z);
            spawned.Add(ob);
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
  
            spawned[i].transform.position = new Vector3(x, localPosition.y, z);
        }
    }
}
