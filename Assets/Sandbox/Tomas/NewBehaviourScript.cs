using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject yay;
    private AudioSource audioSource;
    public AudioQueue queue;
    void Start()
    {
        //queue = yay.GetComponent<AudioQueue>();
        audioSource = GetComponent<AudioSource>();
    }

    private void DebugWin()
    {
        print("Winner Winner Chicken Dinner");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            queue.Play(audioSource);
        }
    }
}
