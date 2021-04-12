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
        Debug.LogWarning("Event instance set");
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
    public event Action<KEY> OnItemHighlight;
    public event Action<KEY> OnItemHighlightOff;
    public event Action<SCENE> OnPassiveCall; 
    public event Action OnDisableMovement;
    public event Action OnEnableMovement;
    public event Action OnDisableJumping;
    public event Action OnEnableJumping;
    public event Action OnDisablePointer;
    public event Action OnSetPhysicsPointer;
    public event Action OnSetRATS;
    public event Action OnSetUIPointer;
    public event Action OnRATSInterrupt;
    public event Action OnRATSNextStep;
    public event Action<bool> OnHurtScreen;
    public event Action<bool> OnPauseScene;
    public event Action OnUpdateVideoSettingsUI;
    public event Action OnApplyVideoSettings;
    
    public event Action OnUpdateComfortSettingsUI;
    public event Action OnPauseGame;
    public event Action OnResumeGame;
    
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
    public void DeHighlightItem(KEY item)
    {
        if (OnItemHighlightOff != null)
        {
            OnItemHighlightOff(item);
        }
        else
        {
            Debug.LogError("OnItemHighlightOff is Null");
        }
    }

    public void HurtScreen(bool enter)
    {
        if (OnHurtScreen != null)
        {
            OnHurtScreen(enter);
        }
        else
        {
            Debug.LogError("OnHurtScreen is Null");
        }
    }

    public void PlayPassive(SCENE scene)
    {
        if (OnPassiveCall != null)
        {
            OnPassiveCall(scene);
        }
        else
        {
            Debug.LogError("OnPassiveCall is Null");
        }
    }

    public void PauseNarration(bool pause)
    {
        if (OnPauseScene != null)
        {
            OnPauseScene(pause);
        }
        else
        {
            Debug.LogError("OnPause is Null");
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
    
    public virtual void SetRATS()
    {
        OnSetRATS?.Invoke();
    }

    public virtual void RATSInterrupt()
    {
        OnRATSInterrupt?.Invoke();
    }
    
    public virtual void RATSNextStep()
    {
        OnRATSNextStep?.Invoke();
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
    
    public virtual void EnableAllInput(PointerState state)
    {
        //TODO: Change to enable physics by default
        EnableMovement();
        EnableJumping();

        switch (state)
        {
            case PointerState.PhysicsPointer:
                SetPhysicsPointer();
                break;
            case PointerState.RATS:
                SetRATS();
                break;
            case PointerState.CanvasPointer:
                SetUIPointer();
                break;
            case PointerState.Disabled:
                DisablePointer();
                break;
        }
    }

    public virtual void UpdateVideoSettingsUI()
    {
        OnUpdateVideoSettingsUI?.Invoke();
    }
    
    public virtual void ApplyVideoSettings()
    {
        OnApplyVideoSettings?.Invoke();
    }
    
    public virtual void UpdateComfortSettingsUI()
    {
        // OnUpdateComfortSettingsUI?.Invoke();
        if (OnUpdateComfortSettingsUI != null)
            OnUpdateComfortSettingsUI.Invoke();
        else
            Debug.LogError("OnUpdateComfortSettingsUI is Null");
    }

    public virtual void PauseGame()
    {
        OnPauseGame?.Invoke();
        
        PauseNarration(true);
        DisableAllInput();
        SetUIPointer();

        Time.timeScale = 0;
        AudioListener.pause = true;
    }
    
    public virtual void ResumeGame(PointerState state)
    {
        OnResumeGame?.Invoke();
        
        EnableAllInput(state);
        PauseNarration(false);
        
        Time.timeScale = 1;
        AudioListener.pause = false;
    }
}

