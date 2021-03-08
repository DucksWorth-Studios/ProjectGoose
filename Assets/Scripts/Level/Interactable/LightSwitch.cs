using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [Tooltip("The Light of the lamp")]
    public Light bulb;

    [Tooltip("The mesh renderer of the bulb mesh")]
    public MeshRenderer bulbMesh;

    [Tooltip("the switch of the lamp")]
    public GameObject switchObj;

    public Material illuminatedMaterial;
    public Material unLitMaterial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Called when the player presses the light switch
    /// Will change the emisive material on the bulb and change the siwtch state
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //Check the hand has the Player layer
        if(other.gameObject.layer == 8)
        {
            if(bulb.enabled)
            {
                bulb.enabled = false;
                bulbMesh.material = unLitMaterial;
                switchObj.transform.localEulerAngles = new Vector3(25, 0, 0);
            }
            else
            {
                bulb.enabled = true;
                bulbMesh.material = illuminatedMaterial;
                switchObj.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
        }
    }
}
