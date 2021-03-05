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
public enum SCENE { ONE, TWO, THREE, FOUR, FIVE, SIX, FOURP2, SUITCASE, CRADLE, FIGURE, JPLAQUE, FPLAQUE, 
    BBURNER, FRAME, NOTEPAD, LAMP, COFFEEMUG, SHBOOK,  ABOOK, COMPASS, SAFE, ENTEROFFICE, WINE, DIPLOMA, DUCKSWORTH};

public enum PointerState
{
    PhysicsPointer,
    NotRussels,
    CanvasPointer,
    Disabled
}


//Button Enum
public enum ButtonEnum { CLOUDREMOVE, NEGATOR, RESET };


public enum Narration { PASSIVE, MAIN };
public class AppData : MonoBehaviour
{
    /*---Floats---*/
    public static float fadeTimeOut = 2f;
    public static float cloudSurviveTime = 27f;
    public static float timeToDisplayRed = 2f;
    public static float timeToClear = 3f;
    public static float mixLeniancey = 0.01f;
    // Delay from survival to item destruction
    public static float cloudSurviveTimeDelay = 3f;

    /*--Colors--*/
    public static Color[] chemicalSteps = { new Color(0.5f, 0.8555f, 0.5f, 1f)};

    /*---Strings---*/
    public static string ignoreTag = "Ignore";
    public static string elementTag = "Element";
    public static string chemicalTag = "Chemical";
    public static string playerTag = "Player";

    /*---DecayMaterial.cs---*/
    public static float Opaque = 1;
    public static float Transparent = 0;
    
    /*---Loading---*/
    public static readonly string SceneToLoad = "LabCompound";
    // public static string sceneToLoad = "VRPlayer-v2-Test";
    // Used for sandbox scenes
    public static readonly Vector3 DefaultPosition = new Vector3(0, 0, 0);
    public static readonly Quaternion DefaultRotation = new Quaternion(0.0f, 0.0f, 0.0f, 1f);
    
    /*---NotRussels---*/
    public static readonly int BufferAllocation = 5;
    // Bit shift the index of the layer (9) to get a bit mask
    public static readonly int InteractableLayerMask = 1 << 9;
}
