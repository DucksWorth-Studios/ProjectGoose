using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author: Tomas
/// Simple Event System for the Game.
/// Created as a Singleton as it will only need to be created once in the game.
/// </summary>
public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    private void Awake()
    {
        //Creates a singleton
        instance = this;
    }
    //All events stored here
    public event Action OnTestEventCall;
    public event Action<Color> OnTestEventCallParam;
    public event Action OnTimeJump;
    public event Action OnTimeJumpButtonPressed;
    public event Action OnNegatorItemJump;
    public event Action<Sound> OnPlaySound;
    public event Action<Sound> OnStopSound;
    public event Action<Sound,bool> OnPlayOneSound;
    public event Action OnLoseGame;
    public event Action OnWinGame;
    public event Action<ButtonEnum> OnButtonPress;
    public event Action<Snap> OnItemSnap;
    public event Action<STAGE> OnProgress;
    public event Action<bool> OnFadeScreen;
    public event Action<KEY> OnItemHighlight;

    public event Action OnDisableMovement;
    public event Action OnEnableMovement;
    public event Action OnDisableJumping;
    public event Action OnEnableJumping;
    public event Action OnDisablePointer;
    public event Action OnSetPhysicsPointer;
    public event Action OnSetUIPointer;
    
    //This is a model for an event
    public void TestEventCall()
    {
        // we can use ?.invoke to call our events but for debug reasons this allows quick access
        if (OnTestEventCall != null)
        {
            OnTestEventCall();
        }
        else
        {
            Debug.LogError("testEventCall is Null");
        }
    }

    //Simple Demo of Passing Params
    public void TestEventCallParam(Color color)
    {
        if (OnTestEventCall != null)
        {
            OnTestEventCallParam(color);
        }
        else
        {
            Debug.LogError("testEventCallParam is Null");
        }
    }


    public void TimeJump()
    {
        OnTimeJump?.Invoke();
    }

    public void TimeJumpButton()
    {
        OnTimeJumpButtonPressed?.Invoke();
    }

    public void NegatorItemJump()
    {
        if (OnNegatorItemJump != null)
        {
            OnNegatorItemJump();
        }
        else
        {
            Debug.LogError("Time Jump is Null");
        }
    }

    public void PlaySound(Sound audio)
    {
        
        if (OnPlaySound != null)
        {
            OnPlaySound(audio);
        }
        else
        {
            Debug.LogError("PlaySound is Null");
        }
    }

    public void StopSound(Sound audio)
    {

        if (OnStopSound != null)
        {
            OnStopSound(audio);
        }
        else
        {
            Debug.LogError("OnStopSound is Null");
        }
    }

    public void LoseGame()
    {
        if (OnLoseGame != null)
        {
            OnLoseGame();
        }
        else
        {
            Debug.LogError("LoseGame is Null");
        }
    }

    public void WinGame()
    {
        if (OnWinGame != null)
        {
            OnWinGame();
        }
        else
        {
            Debug.LogError("WinGame is Null");
        }
    }

    public void PressButton(ButtonEnum buttonToPress)
    {
        if (OnButtonPress != null)
        {
            OnButtonPress(buttonToPress);
        }
        else
        {
            Debug.LogError("OnButtonPress is Null");
        }
    }

    public void SnappedItem(Snap itemSnapped)
    {
        if (OnItemSnap != null)
        {
            OnItemSnap(itemSnapped);
        }
        else
        {
            Debug.LogError("OnItemSnap is Null");
        }
    }

    public void PlayOneSound(Sound sound, bool firstSound)
    {
        if (OnPlayOneSound != null)
        {
            OnPlayOneSound(sound,firstSound);
        }
        else
        {
            Debug.LogError("OnPlayOneSound is Null");
        }
    }

    public void Progress(STAGE stage)
    {
        if (OnProgress != null)
        {
            OnProgress(stage);
        }
        else
        {
            Debug.LogError("OnProgress is Null");
        }
    }

    public void Fade(bool fadeOut)
    {
        if (OnFadeScreen != null)
        {
            OnFadeScreen(fadeOut);
        }
        else
        {
            Debug.LogError("OnFadeScreen is Null");
        }
    }

    public void HighlightItem(KEY item)
    {
        if (OnItemHighlight != null)
        {
            OnItemHighlight(item);
        }
        else
        {
            Debug.LogError("OnItemHighlight is Null");
        }
    }

    public virtual void DisableMovement()
    {
        OnDisableMovement?.Invoke();
    }

    public virtual void EnableMovement()
    {
        OnEnableMovement?.Invoke();
    }

    public virtual void DisableJumping()
    {
        OnDisableJumping?.Invoke();
    }

    public virtual void EnableJumping()
    {
        OnEnableJumping?.Invoke();
    }

    public virtual void DisablePointer()
    {
        OnDisablePointer?.Invoke();
    }

    public virtual void SetPhysicsPointer()
    {
        OnSetPhysicsPointer?.Invoke();
    }

    public virtual void SetUIPointer()
    {
        OnSetUIPointer?.Invoke();
    }

    public virtual void DisableAllInput()
    {
        DisableMovement();
        DisableJumping();
        DisablePointer();
    }
    
    public virtual void EnableAllInput(PointerState state = PointerState.Disabled)
    {
        //TODO: Change to enable physics by default
        EnableMovement();
        EnableJumping();

        switch (state)
        {
            case PointerState.PhysicsPointer:
                SetPhysicsPointer();
                break;
            case PointerState.CanvasPointer:
                SetUIPointer();
                break;
            default:
                DisablePointer();
                break;
        }
    }
}

