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
            print("Hello");
            loadingScreen.gameObject.SetActive(true);
            //SOUND
        }
    }

    private void loading()
    {
        if(loadingScreen.gameObject.activeSelf)
        {
            passedTime += Time.deltaTime;
            loadingScreen.fillAmount = passedTime / loadTime;
            if (passedTime >= loadTime)
            {
                displayFormula();
            }
        }
    }

    private void displayFormula()
    {
        instructions.gameObject.SetActive(true);
        loadingScreen.gameObject.SetActive(false);
        //SOUND
    }
    void Update()
    {
        loading();
    }

}
