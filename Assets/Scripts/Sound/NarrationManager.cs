using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Author Tomas
/// This will be used to control narrative scenes. Each scene is a dedicated Audio Queue
/// </summary>
public class NarrationManager : MonoBehaviour
{
    //Lines For Each Scene will be stored in a prefab audiQueue
    [Tooltip("Narration Disabled")]
    public bool IsDisabled = false;

    //SCENES
    [Tooltip("Queue For Scene One")]
    public AudioQueue sceneOne;
    [Tooltip("Queue For Scene Two")]
    public AudioQueue sceneTwo;
    [Tooltip("Queue For Scene Three")]
    public AudioQueue sceneThree;
    [Tooltip("Queue For Scene Four")]
    public AudioQueue sceneFour;
    [Tooltip("Queue For Scene FourP2")]
    public AudioQueue sceneFourP2;
    [Tooltip("Queue For Scene Five")]
    public AudioQueue sceneFive;
    [Tooltip("Queue For Scene Six")]
    public AudioQueue sceneSix;

    //Passive
    [Tooltip("Queue For SuitCase Scene")]
    public AudioQueue suitcaseScene;
    [Tooltip("Queue For Cradle Scene")]
    public AudioQueue cradleScene;
    [Tooltip("Queue For Figure Scene")]
    public AudioQueue figureScene;
    [Tooltip("Queue For Jamie Plaque Scene")]
    public AudioQueue jplaqueScene;
    [Tooltip("Queue For Francis Plaque Scene")]
    public AudioQueue fplaqueScene;
    [Tooltip("Queue For Burner Scene")]
    public AudioQueue BunsenBurnerScene;
    [Tooltip("Queue For Frame Scene")]
    public AudioQueue frameScene;
    [Tooltip("Queue For Notepad Scene")]
    public AudioQueue notepadScene;
    [Tooltip("Queue For Lamp Scene")]
    public AudioQueue lampScene;
    [Tooltip("Queue For Mug Scene")]
    public AudioQueue mugScene;
    [Tooltip("Queue For SHBook Scene")]
    public AudioQueue shBookScene;
    [Tooltip("Queue For ABook Scene")]
    public AudioQueue aBookScene;
    [Tooltip("Queue For Compass Scene")]
    public AudioQueue compassScene;
    [Tooltip("Queue For Safe Scene")]
    public AudioQueue safeScene;
    [Tooltip("Queue For EnterOffice Scene")]
    public AudioQueue officeScene;
    [Tooltip("Queue For Wine Scene")]
    public AudioQueue wineScene;
    [Tooltip("Queue For Diploma Scene")]
    public AudioQueue diplomaScene;
    [Tooltip("Queue For DucksWorth Scene")]
    public AudioQueue ducksScene;




    //Private variables
    private AudioSource activeNarration;
    private AudioQueue activeQueue;
    private List<int> passivePlayed;
    private bool InPast = true;
    private bool DisableAllButPassive = true;
    void Start()
    {
        passivePlayed = new List<int>();
        activeNarration = GetComponent<AudioSource>();
        EventManager.instance.OnTimeJump += JumpInteference;
        EventManager.instance.OnLoseGame += StopScene;
        EventManager.instance.OnPassiveCall += NarrationCall;
    }

    //Switch is sued to define what scene will be played.
    public void NarrationCall(SCENE line)
    {
        if(!IsDisabled)
        {
            switch (line)
            {
                case SCENE.ONE:
                    PlayScene(sceneOne);
                    break;
                case SCENE.TWO:
                    PlayScene(sceneTwo);
                    break;
                case SCENE.THREE:
                    PlayScene(sceneThree);
                    break;
                case SCENE.FOUR:
                    if(DisableAllButPassive)
                    {
                        DisableAllButPassive = false;
                        PlayScene(sceneFour);
                        DisableAllButPassive = true;
                    }
                    else
                    {
                        PlayScene(sceneFour);
                    }
                    break;
                case SCENE.FOURP2:
                    PlayScene(sceneFourP2);
                    break;
                case SCENE.FIVE:
                    PlayScene(sceneFive);
                    break;
                case SCENE.SIX:
                    PlayScene(sceneSix);
                    break;
                case SCENE.SUITCASE:
                    PlayPassiveScene(line, suitcaseScene);
                    break;
                case SCENE.CRADLE:
                    PlayPassiveScene(line, cradleScene);
                    break;
                case SCENE.FIGURE:
                    PlayPassiveScene(line, figureScene);
                    break;
                case SCENE.JPLAQUE:
                    PlayPassiveScene(line, jplaqueScene);
                    break;
                case SCENE.FPLAQUE:
                    PlayPassiveScene(line, fplaqueScene);
                    break;
                case SCENE.BBURNER:
                    PlayPassiveScene(line, BunsenBurnerScene);
                    break;
                case SCENE.FRAME:
                    PlayPassiveScene(line, frameScene);
                    break;
                case SCENE.NOTEPAD:
                    PlayPassiveScene(line, notepadScene);
                    break;
                case SCENE.LAMP:
                    PlayPassiveScene(line, lampScene);
                    break;
                case SCENE.COFFEEMUG:
                    PlayPassiveScene(line, mugScene);
                    break;
                case SCENE.SHBOOK:
                    PlayPassiveScene(line, shBookScene);
                    break;
                case SCENE.ABOOK:
                    PlayPassiveScene(line, aBookScene);
                    break;
                case SCENE.COMPASS:
                    PlayPassiveScene(line, compassScene);
                    break;
                case SCENE.SAFE:
                    PlayPassiveScene(line, safeScene);
                    break;
                case SCENE.ENTEROFFICE:
                    PlayPassiveScene(line, officeScene);
                    break;
                case SCENE.WINE:
                    PlayPassiveScene(line, wineScene);
                    break;
                case SCENE.DIPLOMA:
                    PlayPassiveScene(line, diplomaScene);
                    break;
                case SCENE.DUCKSWORTH:
                    PlayPassiveScene(line, ducksScene);
                    break;
                default:
                    break;
            }

        }
        
    }

    
    private void PlayScene(AudioQueue queue)
    {
        if (!DisableAllButPassive)
        {
            if (activeQueue != null)
            {
                StopScene();
            }

            queue.Play(activeNarration);
            activeQueue = queue;
        }
        else
        {
            activeQueue = queue;
            queue.Play(activeNarration);
            queue.Stop();
            activeQueue.IsFinished = true;
        }
        
    }

    private void StopScene()
    {
        if(!IsDisabled)
        {
            activeQueue.Stop();
        }

    }
    /// <summary>
    /// Checks that we dont interupt a main scene otherwise plays the scene
    /// </summary>
    /// <param name="queue">Scene to play</param>
    private void PlayPassiveScene(SCENE scene,AudioQueue queue)
    {
        if(activeQueue.narrationType == Narration.MAIN && !activeQueue.IsFinished)
        {
            //dont do anything
        }
        else if(queue.IsFinished)
        {
            //dont do anything
        }
        else if(passivePlayed.Contains((int) scene))
        {

        }
        else
        {
            passivePlayed.Add((int)scene);
            StopScene();
            queue.Play(activeNarration);
            activeQueue = queue;
        }
    }

    private void PauseScene()
    {
        activeQueue.Pause();
    }

    private void UnPauseScene()
    {
        activeQueue.UnPause();
    }

    /// <summary>
    /// Will Be Event To Pause Scene
    /// </summary>
    /// <param name="Pause"> If true scene will be paused. If False Scene will be unpaused</param>
    private void PauseEvent(bool Pause)
    {
        if(Pause)
        {
            PauseScene();
        }
        else
        {
            UnPauseScene();
        }
    }
    //Teleport Jump will intefere with clips keep track of tate we are in
    public void JumpInteference()
    {
        if(activeQueue != null)
        {
            activeQueue.InteruptLines();
        }

    }


    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.O))
    //    {
    //        NarrationCall(SCENE.FOUR);
    //    }
    //}
}
