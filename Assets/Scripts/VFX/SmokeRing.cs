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
    
    [Tooltip("The limit for how small the fianll fog radius should be")]
    public float spawnRadiusLimit = 1;
    
    [Tooltip("Time is spawn radius, value is spawned effects.")]
    public AnimationCurve spawnCurve = AnimationCurve.Linear(0, 10, 20, 5);
    
    [Tooltip("The speed the smoke should move at")]
    public float moveSpeed = 5;

    private int initialSpawnLimit;
    private float initialSpawnRadius;
    private List<GameObject> spawned;
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
                // StopCoroutine("CloseIn");
                
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
            Vector3 newPosition = new Vector3(x, localPosition.y, z);
            ob.transform.position = newPosition;
            
            VFXManager vfx = ob.GetComponent<VFXManager>();
            vfx.target = newPosition;
            vfx.moveSpeed = moveSpeed;
            
            ob.name = "Fog-" + i;
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

            VFXManager vfx = spawned[i].GetComponent<VFXManager>();
            vfx.target = new Vector3(x, localPosition.y, z);

            // StartCoroutine(CloseIn(spawned[i], new Vector3(x, localPosition.y, z)));
        }
    }

    private IEnumerator CloseIn(GameObject objectToMove, Vector3 target)
    {
        Debug.Log(objectToMove.name + ": " + target);
        
        // while (objectToMove.transform.position != target)
        while (objectToMove && Vector3.Distance(objectToMove.transform.position, target) > 0.001f)
        {
            // Move our position a step closer to the target.
            float step =  moveSpeed * Time.deltaTime;
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, target, step);

            yield return null;
        }

        Debug.Log("Reached target");
    }
}
