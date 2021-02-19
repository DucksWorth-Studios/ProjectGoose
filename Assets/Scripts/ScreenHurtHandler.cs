using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScreenHurtHandler : MonoBehaviour
{
    private bool ClearScreen = true;
    private bool IsActive = false;
    private float goal = 0.7f;
    private float timeToDisplayHurt = AppData.timeToDisplayRed;
    private float timeToClearHurt = AppData.timeToClear;
    private float timeRemaining = 0;
    public Image screen;
    void Start()
    {
        EventManager.instance.OnHurtScreen += HurtEvent;
    }

    /// <summary>
    /// When this event is called it will determine wether the indicator is to be turned on or cleared.
    /// </summary>
    /// <param name="enterCloud">Has the player entereed the cloud if false they are leaving</param>
    private void HurtEvent(bool enterCloud)
    {
        if(enterCloud)
        {
            IsActive = true;
            ClearScreen = false;
        }
        else
        {
            IsActive = true;
            ClearScreen = true;
        }
    }
    /// <summary>
    /// Starts Displaying the hurt indicator
    /// </summary>
    private void HurtIndicator()
    {
        timeRemaining += Time.deltaTime;

        //Percentage of time to full effect
        float percentage = timeRemaining / timeToDisplayHurt;

        //apply that percentage to the opacity
        float opacity = percentage * goal;

        //if timeremaining is larger
        if (timeRemaining >= timeToDisplayHurt)
        {
            timeRemaining = timeToClearHurt;
            screen.color = new Color(1,0,0,goal);
            IsActive = false;
        }
        else
        {
            screen.color = new Color(1, 0, 0, opacity);
        }
    }

    /// <summary>
    /// Clears the indicator
    /// </summary>
    private void ClearIndicator()
    {
        timeRemaining -= Time.deltaTime;
        

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            screen.color = new Color(1, 0, 0, 0);
            IsActive = false;
        }
        else
        {
            float percentage = timeRemaining / timeToClearHurt;
            float opacity = percentage * goal;
            screen.color = new Color(1, 0, 0, opacity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(IsActive)
        {
            if(ClearScreen)
            {
                ClearIndicator();
            }
            else
            {
                HurtIndicator();
            }
        }
    }
}
