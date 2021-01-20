using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NARRATION { START, USB, FORMULA,CHEMICAL, HALFLIFE, COMPLETEFORMULA, DANGER };
/// <summary>
/// Author Tomas
/// Active Narration concerns important story and game elements
/// Passive narration  is small lore based items.
/// </summary>
public class NarrationManager : MonoBehaviour
{
    [Tooltip("Clip To Play")]
    public AudioClip[] clips;

    //Private variables
    private AudioSource activeNarration;
    private AudioSource passiveNarration;
    private bool activePaused = false;
    private bool passivePaused = false;
    private bool InPast = true;
    void Start()
    {
        activeNarration = GetComponent<AudioSource>();
        passiveNarration = GetComponent<AudioSource>();
        EventManager.instance.OnTimeJump += JumpInteference;
    }

    //Switch is sued to define what clip will be played.
    public void narrationCall(NARRATION line)
    {
        switch(line)
        {
            case NARRATION.START:

                break;
            default:
                break;
        }
    }

    //A Main clip is more important than a passive one therefore stop passive.
    private void playMainClip(AudioClip clip)
    {
        activeNarration.clip = clip;
        if(passiveNarration.isPlaying)
        {
            passiveNarration.Stop();
        }
        activeNarration.Play();
    }

    //Only if active narration is not playing
    private void playPassiveClip(AudioClip clip)
    {
        if(!activeNarration.isPlaying)
        {
            passiveNarration.clip = clip;
            activeNarration.Play();
        }
    }

    //Pauses One By One
    private void PauseClips()
    {
        if (activeNarration.isPlaying)
        {
            activeNarration.Pause();
        }
        else
        {
            passiveNarration.Pause();
        }
    }

    //Unpauses one by one
    private void UnPauseClips()
    {
        activeNarration.UnPause();
        if(!activeNarration.isPlaying)
        {
            passiveNarration.UnPause();
        }
    }

    //Teleport Jump will intefere with clips keep track of tate we are in
    private void JumpInteference()
    {
        if(InPast)
        {
            InPast = false;
            PauseClips();
        }
        else
        {
            InPast = true;
            UnPauseClips();
        }
    }
    //Not used Currently
    //void Update()
    //{
        
    //}
}
