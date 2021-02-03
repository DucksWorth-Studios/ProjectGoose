using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Tomas
/// Used to control the computer loading the formula
/// </summary>
public class Computerhandler : MonoBehaviour
{
    [Tooltip("The Loading Object")]
    public Image loadingScreen;

    [Tooltip("The Instructions To Reveal")]
    public Image instructions;

    [Tooltip("Time Takes to Load")]
    public int loadTime;
    //Time passed
    private float passedTime;

    void Start()
    {
        EventManager.instance.OnItemSnap += EnableScreen;
    }

    private void EnableScreen(Snap itemSnapped)
    {
        if(itemSnapped == Snap.USB)
        {
            loadingScreen.gameObject.SetActive(true);
            //SOUND
            EventManager.instance.PlaySound(Sound.USB);
            print("USB LOAD");
        }
    }

    private void Loading()
    {
        if(loadingScreen.gameObject.activeSelf)
        {
            passedTime += Time.deltaTime;
            loadingScreen.fillAmount = passedTime / loadTime;
            if (loadingScreen.fillAmount >= 1)
            {
                DisplayFormula();
            }
        }
    }

    private void DisplayFormula()
    {
        loadingScreen.gameObject.SetActive(false);
        instructions.gameObject.SetActive(true);
        
        //SOUND
    }
    void Update()
    {
        Loading();
    }

}
