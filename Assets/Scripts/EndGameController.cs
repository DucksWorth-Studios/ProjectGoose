using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
/// <summary>
/// Author: Tomas
/// Controls Players endgame. Changes the position to one of two small rooms.
/// </summary>
public class EndGameController : MonoBehaviour
{
    [Tooltip("TimeOut For Transition")]
    public float timeOut = 10f;
    [Tooltip("Object For Lose Position")]
    public GameObject badPosition;
    [Tooltip("Object for Win Position")]
    public GameObject goodPosition;
    private GameObject VRplayer;

    void Start()
    {
        VRplayer = Player.instance.gameObject;
        EventManager.instance.OnLoseGame += LoseGameEvent;
        EventManager.instance.OnWinGame += WinGameEvent;
    }

    public void WinGameEvent()
    {
        StartCoroutine(GameOverGood());
    }

    public void LoseGameEvent()
    {
        StartCoroutine(GameOverBad());
    }
    /// <summary>
    /// CoRoutines are used to ensure it fades out changes position then fades in again
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameOverGood()
    {
        EventManager.instance.DisableAllInput();
        EventManager.instance.SetUIPointer();

        Valve.VR.SteamVR_Fade.View(Color.black, AppData.fadeTimeOut);
        VRplayer.GetComponent<VRPlayerDimensionJump>().enabled = false;
        yield return new WaitForSeconds(3);
        VRplayer.transform.position = goodPosition.transform.position;
        Valve.VR.SteamVR_Fade.View(Color.clear, AppData.fadeTimeOut);
    }

    private IEnumerator GameOverBad()
    {
        print("Got HEre");
        EventManager.instance.DisableAllInput();
        EventManager.instance.SetUIPointer();

        Valve.VR.SteamVR_Fade.View(Color.black, AppData.fadeTimeOut);
        VRplayer.GetComponent<VRPlayerDimensionJump>().enabled = false;
        VRplayer.GetComponent<SmokeRing>().enabled = false;
        yield return new WaitForSeconds(3);
        print("Got HEre");
        VRplayer.transform.position = badPosition.transform.position;
        Valve.VR.SteamVR_Fade.View(Color.clear, AppData.fadeTimeOut);

    }
    //Debug for testing
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            EventManager.instance.WinGame();
        }
    }
}
