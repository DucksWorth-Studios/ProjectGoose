﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SCENE { ONE, TWO, THREE, FOUR, FIVE, SIX };
/// <summary>
/// Author Tomas
/// This will be used to control narrative scenes. Each scene is a dedicated Audio Queue
/// </summary>
public class NarrationManager : MonoBehaviour
{
    //Lines For Each Scene will be stored in a prefab audiQueue    
    public AudioQueue sceneOne;
    public AudioQueue sceneTwo;
    public AudioQueue sceneThree;
    public AudioQueue sceneFour;
    public AudioQueue sceneFive;
    public AudioQueue sceneSix;

    //Private variables
    private AudioSource activeNarration;
    private AudioQueue activeQueue;
    private bool InPast = true;
    void Start()
    {
        activeNarration = GetComponent<AudioSource>();
        //EventManager.instance.OnTimeJump += JumpInteference;
    }

    //Switch is sued to define what scene will be played.
    public void narrationCall(SCENE line)
    {
        switch(line)
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

    
    private void PlayScene(AudioQueue queue)
    {
        queue.Play(activeNarration);
        activeQueue = queue;
    }

    private void StopScene()
    {
        activeQueue.Stop();
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