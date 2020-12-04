using UnityEngine;

/// <summary>
/// Author: Cameron Scholes
/// The script will spawn the smoke effect and slowly close in on the player
/// </summary>
public class SmokeRing : MonoBehaviour
{
    public GameObject smokePrefab;
    public int spawnLimit = 5;
    public float spawnRadius = 5;

    
    private int initialSpawnLimit;
    private float initialSpawnRadius;
    private GameObject[] spawned;

    void Start()
    {
        initialSpawnLimit = spawnLimit;
        initialSpawnRadius = spawnRadius;
        EventManager.instance.OnTimeJump += UpdateSmokeEffect;
    }

    private void UpdateSmokeEffect()
    {
        if (spawned != null && spawned.Length > 0)
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
            // TODO: Chnage this to be based on watch timer
            InvokeRepeating("ShrinkFogArea", 1, 1);
        }
    }

    void ShrinkFogArea()
    {
        if (spawnRadius > 1)
        {
            spawnRadius -= 1;

            Destroy();
            Reset();
        } else
            CancelInvoke("ShrinkFogArea");
    }
    void Destroy()
    {
        foreach (GameObject ob in spawned)
            Destroy(ob);

        spawned = null;
    }
    
    void Reset()
    {
        spawned = new GameObject[spawnLimit];
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
  
            GameObject ob = Instantiate(smokePrefab, transform, true);
            ob.transform.position = new Vector3(x, localPosition.y, z);
            spawned[i] = ob;
        }
    }
}
