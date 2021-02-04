using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Author: Cameron Scholes & VR With Andrew
///
/// Replaces Unity button script with quick colour changes
/// 
/// Resource: https://www.youtube.com/watch?v=vNqHRD4sqPc
/// </summary>

public class ButtonTransitioner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, 
                                    IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public Color32 normalColour = Color.white;
    public Color32 hoverColour = Color.blue;
    public Color32 downColour = Color.red;
    
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = hoverColour;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = normalColour;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        image.color = downColour;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        image.color = hoverColour;
    }
}