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
    // Start is called before the first frame update
    public Image loadingScreen;
    public Image instructions;
    public int loadTime;
    private float passedTime;

    void Start()
    {
        EventManager.instance.OnItemSnap += enableScreen;
    }

    private void enableScreen(Snap itemSnapped)
    {
        if(itemSnapped == Snap.USB)
        {
            loadingScreen.gameObject.SetActive(true);
            //SOUND
            EventManager.instance.PlaySound(Sound.USB);
            print("USB LOAD");
        }
    }

    private void loading()
    {
        if(loadingScreen.gameObject.activeSelf)
        {
            passedTime += Time.deltaTime;
            loadingScreen.fillAmount = passedTime / loadTime;
            if (loadingScreen.fillAmount >= 1)
            {
                displayFormula();
            }
        }
    }

    private void displayFormula()
    {
        loadingScreen.gameObject.SetActive(false);
        instructions.gameObject.SetActive(true);
        
        //SOUND
    }
    void Update()
    {
        loading();
    }

}
