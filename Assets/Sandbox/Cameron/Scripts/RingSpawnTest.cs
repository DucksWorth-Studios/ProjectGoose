using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSpawnTest : MonoBehaviour
{
    public GameObject objectToSpawn;
    public int spawnLimit = 5;
    public float spawnRadius = 5;

    private GameObject[] spawned;
    private int initialSpawnLimit;
    private float initialSpawnRadius;
    
    void Awake()
    {
        Reset();
    }

    void Update()
    {
        // TODO: This is just for testing and should be removed
        if (initialSpawnLimit != spawnLimit)
            Destroy();
        else if (initialSpawnRadius != spawnRadius)
            Destroy();
    }

    void Destroy()
    {
        foreach (GameObject ob in spawned)
            Destroy(ob);
        
        Reset();
    }
    
    void Reset()
    {
        spawned = new GameObject[spawnLimit];
        initialSpawnLimit = spawnLimit;
        initialSpawnRadius = spawnRadius;

        SpawnObjects();
    }
    
    private void SpawnObjects()
    {
        for (int i = 0; i < spawnLimit; i++)
        {
            Vector3 localPosition = transform.position;
            
            float theta = i * 2 * Mathf.PI / spawnLimit;
            float x = Mathf.Sin(theta)*spawnRadius + localPosition.x;
            float z = Mathf.Cos(theta)*spawnRadius + localPosition.z;
  
            GameObject ob = Instantiate(objectToSpawn, transform, true);
            ob.transform.position = new Vector3(x, 0, z);
            spawned[i] = ob;
        }
    }
}
