using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficScreen : MonoBehaviour
{
    Vector3 RedLightOn = new Vector3(-403,56,80);
    Vector3 GreenLightOn = new Vector3((float)-402.598,56,80);


    public void Update()
    {
        string A = GameObject.Find("Car").GetComponent<DrivingAgent>().serverMsg;

        transform.position = GreenLightOn;

        if(A.Contains("green_light"))
        {
            transform.position = GreenLightOn;
        }
        else if(A.Contains("red_light"))
        {
            transform.position = RedLightOn;
        }       
    }
}
