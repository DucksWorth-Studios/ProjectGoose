using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Computerhandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Image loadingScreen;
    public Image instructions;
    void Start()
    {
        EventManager.instance.OnItemSnap += enableScreen;
    }

    private void enableScreen(Snap itemSnapped)
    {
        if(itemSnapped == Snap.USB)
        {
            loadingScreen.enabled = true;
            //StartCoRoutine
            //SOUND
        }
    }

    IEnumerator beginLoading()
    {
        yield return null;
    }
}
