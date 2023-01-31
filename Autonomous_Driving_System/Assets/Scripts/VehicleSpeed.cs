using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VehicleSpeed : MonoBehaviour
{
    private Vector3 lastPosition;
    public float Speed;

    [SerializeField]
    public TextMeshProUGUI meterPerSecond, kilometersPerHour;

    float GetSpeed()
    {
        float speed = (((transform.position - lastPosition).magnitude) / Time.deltaTime);
        lastPosition = transform.position;

        return speed;
    }


    void FixedUpdate()
    {
        Speed = GetSpeed();
        meterPerSecond.text = string.Format("{0:00.00} m/s", Speed);
        kilometersPerHour.text = string.Format("{0:00.00} km/h", Speed * 3.6f);
    }

}
