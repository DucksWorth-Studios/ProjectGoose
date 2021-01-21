using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public SCENE scene;
    public NarrationManager narration;
    void Start()
    {

    }

    private void DebugWin()
    {
        print("Winner Winner Chicken Dinner");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            narration.narrationCall(scene);
        }
    }
}
