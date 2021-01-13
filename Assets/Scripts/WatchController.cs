using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
/// <summary>
/// Author: Tomas
/// Simple Script that Changes visibility of watch Menu
/// </summary>
public class WatchController : MonoBehaviour
{
    public GameObject menu;
    private bool state = true;
    private void Awake()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RightHand")
        {
            changeState();
        }
    }

    private void changeState()
    {
        if (state == true)
        {
            state = false;
            menu.SetActive(state);
        }
        else
        {
            state = true;
            menu.SetActive(state);
        }
    }
}
