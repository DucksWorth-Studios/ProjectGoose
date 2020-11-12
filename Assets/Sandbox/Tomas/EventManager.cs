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

}
