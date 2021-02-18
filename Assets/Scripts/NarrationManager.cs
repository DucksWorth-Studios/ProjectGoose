using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Author Tomas
/// This will be used to control narrative scenes. Each scene is a dedicated Audio Queue
/// </summary>
public class NarrationManager : MonoBehaviour
{
    //Lines For Each Scene will be stored in a prefab audiQueue
    [Tooltip("Queue For Scene One")]
    public AudioQueue sceneOne;
    [Tooltip("Queue For Scene Two")]
    public AudioQueue sceneTwo;
    [Tooltip("Queue For Scene Three")]
    public AudioQueue sceneThree;
    [Tooltip("Queue For Scene Four")]
    public AudioQueue sceneFour;
    [Tooltip("Queue For Scene FourP2")]
    public AudioQueue sceneFourP2;
    [Tooltip("Queue For Scene Five")]
    public AudioQueue sceneFive;
    [Tooltip("Queue For Scene Six")]
    public AudioQueue sceneSix;
    [Tooltip("Narration Disabled")]
    public bool IsDisabled = false;
    //Private variables
    private AudioSource activeNarration;
    private AudioQueue activeQueue;
    private bool InPast = true;
    void Start()
    {
        activeNarration = GetComponent<AudioSource>();
        //EventManager.instance.OnTimeJump += JumpInteference;
        EventManager.instance.OnLoseGame += StopScene;
    }

    //Switch is sued to define what scene will be played.
    public void NarrationCall(SCENE line)
    {
        if(!IsDisabled)
        {
            switch (line)
            {
                case SCENE.ONE:
                    PlayScene(sceneOne);
                    break;
                case SCENE.TWO:
                    PlayScene(sceneTwo);
                    break;
                case SCENE.THREE:
                    PlayScene(sceneThree);
                    break;
                case SCENE.FOUR:
                    PlayScene(sceneFour);
                    break;
                case SCENE.FOURP2:
                    PlayScene(sceneFourP2);
                    break;
                case SCENE.FIVE:
                    PlayScene(sceneFive);
                    break;
                case SCENE.SIX:
                    PlayScene(sceneSix);
                    break;
                default:
                    break;
            }
        }
        
    }

    
    private void PlayScene(AudioQueue queue)
    {
        if(activeQueue != null)
        {
            StopScene();
        }

        queue.Play(activeNarration);
        activeQueue = queue;
    }

    private void StopScene()
    {
        if(!IsDisabled)
        {
            activeQueue.Stop();
        }

    }

    private void PauseScene()
    {
        activeQueue.Pause();
    }

    private void UnPauseScene()
    {
        activeQueue.UnPause();
    }
    //Teleport Jump will intefere with clips keep track of tate we are in
    private void JumpInteference()
    {
        if(InPast)
        {
            InPast = false;
            PauseScene();
        }
        else
        {
            InPast = true;
            UnPauseScene();
        }
    }
    //Not used Currently
    //void Update()
    //{
        
    //}
}
