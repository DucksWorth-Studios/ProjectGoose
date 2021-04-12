using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveBoardManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.OnProgress += UpdateBoard;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateBoard(STAGE stage)
    {

    }
}
