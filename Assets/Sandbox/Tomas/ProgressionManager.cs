using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STAGE { START, FIRSTJUMP, FIRSTRETURN, USB, USBPLUGGED, CHEMICALPUZZLE, ELEMENTPUZZLE,END };
public class ProgressionManager : MonoBehaviour
{
    public NarrationManager narrationManager;
    public static ProgressionManager instance;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Possibly Link To An Event
    }

    //Outside forces dictate what stage it is at.
    //It then calls up the relevant stage
    public void Progress(STAGE stage)
    {
        switch(stage)
        {
            case STAGE.START:
                GameStart();
                break;
            case STAGE.FIRSTJUMP:
                FirstJump();
                break;
            case STAGE.FIRSTRETURN:
                FirstReturn();
                break;
            case STAGE.USB:
                USB();
                break;
            case STAGE.USBPLUGGED:
                USBPlugged();
                break;
            case STAGE.CHEMICALPUZZLE:
                ChemicalPuzzle();
                break;
            case STAGE.ELEMENTPUZZLE:
                ElementPuzzle();
                break;
            case STAGE.END:
                GameEnd();
                break;
            default:
                break;
        }
    }

    private void GameStart()
    {
        narrationManager.narrationCall(SCENE.ONE);
    }

    private void FirstJump()
    {
        narrationManager.narrationCall(SCENE.TWO);
    }

    private void FirstReturn()
    {
        narrationManager.narrationCall(SCENE.THREE);
    }

    private void USB()
    {
        narrationManager.narrationCall(SCENE.FOUR);
    }

    private void USBPlugged()
    {
        narrationManager.narrationCall(SCENE.FIVE);
    }

    private void ChemicalPuzzle()
    {

    }
    private void ElementPuzzle()
    {

    }

    private void GameEnd()
    {
        narrationManager.narrationCall(SCENE.SIX);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
