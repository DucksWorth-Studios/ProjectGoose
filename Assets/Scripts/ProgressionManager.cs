using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProgressionManager : MonoBehaviour
{
    [Tooltip("Reference To Narration Manager")]
    public NarrationManager narrationManager;
    [Tooltip("Narration Disabled")]
    public bool IsDisabled = false;
    private bool InPast = true;
    private bool firstJump = false;
    private bool firstReturn = false;

    // Start is called before the first frame update
    void Start()
    {
        narrationManager.IsDisabled = IsDisabled;
        EventManager.instance.OnTimeJump += JumpTrack;
        EventManager.instance.OnProgress += Progress;
        //Valve.VR.SteamVR_Fade.View(Color.black, 0);
        //StartCoroutine(StartUp());
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
            case STAGE.USBRETURN:
                USBReturn();
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
        narrationManager.NarrationCall(SCENE.ONE);
    }

    private void FirstJump()
    {
        narrationManager.NarrationCall(SCENE.TWO);
    }

    private void FirstReturn()
    {
        narrationManager.NarrationCall(SCENE.THREE);
        EventManager.instance.HighlightItem(KEY.USB);
    }

    private void USB()
    {
        narrationManager.NarrationCall(SCENE.FOUR);
        EventManager.instance.HighlightItem(KEY.USBSLOT);
    }

    private void USBReturn()
    {
        narrationManager.NarrationCall(SCENE.FOURP2);
        //EventManager.instance.HighlightItem(KEY.USBSLOT);
    }

    private void USBPlugged()
    {
        narrationManager.NarrationCall(SCENE.FIVE);
        EventManager.instance.HighlightItem(KEY.CHEMICAL);
    }

    private void ChemicalPuzzle()
    {

    }
    private void ElementPuzzle()
    {

    }

    private void GameEnd()
    {
        narrationManager.NarrationCall(SCENE.SIX);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void JumpTrack()
    {
        if (InPast)
        {
            InPast = false;
            if(!firstJump)
            {
                firstJump = true;
                Progress(STAGE.FIRSTJUMP);
            }
        }
        else
        {
            InPast = true;
            if (!firstReturn)
            {
                firstReturn = true;
                Progress(STAGE.FIRSTRETURN);
            }
        }
    }

    private IEnumerator StartUp()
    {
        Valve.VR.SteamVR_Fade.View(Color.black, 0f);
        yield return new WaitForSeconds(0.000000001f);
        EventManager.instance.Fade(false);
        yield return new WaitForSeconds(2);
        Progress(STAGE.START);
    }
}
