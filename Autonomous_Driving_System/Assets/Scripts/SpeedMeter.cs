using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedMeter : MonoBehaviour
{
    GameObject Chim = GameObject.Find("Chim2");

    public float NS;

    private void Start()
    {
        Chim.transform.Rotate(new Vector3(0, 0, 24));
    }
    void Update()
    {
        NS = GameObject.Find("kilometersPerHour").GetComponent<VehicleSpeed>().Speed * 3.6f;
        Chim.transform.Rotate(new Vector3(0, 0, 24 - NS));  
    }
}
