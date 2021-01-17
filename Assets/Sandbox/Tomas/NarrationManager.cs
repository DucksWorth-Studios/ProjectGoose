using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NARRATION { START, USB, FORMULA,CHEMICAL, HALFLIFE, COMPLETEFORMULA, DANGER };
public class NarrationManager : MonoBehaviour
{
    private AudioSource activeNarration;
    private AudioSource passiveNarration;
    private bool activePaused = false;
    private bool passivePaused = false;

    void Start()
    {
        activeNarration = GetComponent<AudioSource>();
        passiveNarration = GetComponent<AudioSource>();
    }

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
    private void playMainClip(AudioClip clip)
    {
        activeNarration.clip = clip;
        activeNarration.Play();
    }

    private void playPassiveClip(AudioClip clip)
    {
        passiveNarration.clip = clip;
        activeNarration.Play();
    }

    private void unPauseActive()
    {

    }

    private void unPausePassive()
    {

    }

    void Update()
    {
        
    }
}
