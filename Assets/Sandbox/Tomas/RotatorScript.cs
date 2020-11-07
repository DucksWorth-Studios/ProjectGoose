using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorScript : MonoBehaviour
{
    //Rotational Speed
    public float speed = 5f;

    //Forward Direction
    public bool Xaxis = false;
    public bool Yaxis = false;
    public bool Zaxis = false;

    /// <summary>
    /// Rotates Positon using unity editor to dictate what axis to rotate over
    /// </summary>
    void Update()
    {
        //Forward Direction
        if (Xaxis == true && Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Time.deltaTime * speed, 0, 0, Space.Self);
        }
        if (Yaxis == true && Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, Time.deltaTime * speed, 0, Space.Self);
        }
        if (Zaxis == true && Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, Time.deltaTime * speed, Space.Self);
        }

        //Reverse Direction
        if (Xaxis == true && Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Time.deltaTime * speed, 0, 0, Space.Self);
        }
        if (Yaxis == true && Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -Time.deltaTime * speed, 0, Space.Self);
        }
        if (Zaxis == true && Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, -Time.deltaTime * speed, Space.Self);
        }

    }
}
