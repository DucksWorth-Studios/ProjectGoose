using System;
using UnityEngine;

public class PointerEvents : MonoBehaviour
{
    public static PointerEvents current;

    public void Awake()
    {
        current = this;
    }

    public event Action OnPointerEnter;
    public event Action OnPointerExit;
    public event Action OnPointerDown;
    public event Action OnPointerUp;
    public event Action OnPointerClick;

    public void PointerEnter()
    {
        OnPointerEnter?.Invoke();
    }
    
    public void PointerExit()
    {
        OnPointerExit?.Invoke();
    }
    
    public void PointerDown()
    {
        OnPointerDown?.Invoke();
    }
    
    public void PointerUp()
    {
        OnPointerUp?.Invoke();
    }
    
    public void PointerClick()
    {
        OnPointerClick?.Invoke();
    }
}