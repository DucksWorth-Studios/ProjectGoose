using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveBoardManager : MonoBehaviour
{
    public GameObject stage1;
    public GameObject stage2;
    public GameObject chemicalCheckMark;
    public GameObject steriliseCheckMark;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.OnProgress += UpdateBoard;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBoard(STAGE stage)
    {
        switch(stage)
        {
            case STAGE.FIRSTRETURN:
                stage1.SetActive(true);
                break;
            case STAGE.USBPLUGGED:
                stage2.SetActive(true);
                break;
            case STAGE.CHEMICALPUZZLE:
                stage2.SetActive(true);
                chemicalCheckMark.SetActive(true);
                break;
            case STAGE.ELEMENTPUZZLE:
                steriliseCheckMark.SetActive(true);
                break;
            default:
                return;
        }
    }
}
