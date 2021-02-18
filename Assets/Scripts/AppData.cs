using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Used To play Sounds
public enum Sound { DoorSound, Creaking, Breaking, Alarm, Airlock, Walk, Teleport, ItemPickUp, USB, ItemTeleport };

//Used To Define the Stage
public enum STAGE { START, FIRSTJUMP, FIRSTRETURN, USB, USBPLUGGED, CHEMICALPUZZLE, ELEMENTPUZZLE, END, USBRETURN };

//Used For A Snap Event
public enum Snap { USB };

//Key Enum Used TO Control what Items Highlight
public enum KEY { CHEMICAL, USB, USBSLOT };

//Used Define the scene to play
public enum SCENE { ONE, TWO, THREE, FOUR, FIVE, SIX, FOURP2 };

//Button Enum
public enum ButtonEnum { CLOUDREMOVE, NEGATOR };

public class AppData : MonoBehaviour
{
    /*---Floats---*/
    public static float fadeTimeOut = 2f;
    public static float cloudSurviveTime = 27f;
    // Delay from survival to item destruction
    public static float cloudSurviveTimeDelay = 3f;


    /*---Strings---*/
    public static string ignoreTag = "Ignore";
    public static string elementTag = "Element";
    public static string chemicalTag = "Chemical";
    public static string playerTag = "Player";
}
