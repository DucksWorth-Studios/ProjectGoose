using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

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
            EventManager.instance.PlayPassive(SCENE.ABOOK);
            
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            EventManager.instance.Progress(STAGE.USB);

        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            EventManager.instance.PlayPassive(SCENE.BBURNER);

        }
    }
}
