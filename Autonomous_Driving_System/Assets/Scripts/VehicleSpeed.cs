using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Threading;

public class VehicleSpeed : MonoBehaviour
{
    [SerializeField]
    GameObject Chim;

    private Vector3 lastPosition;
    public float Speed;

    [SerializeField]
    public TextMeshProUGUI meterPerSecond, kilometersPerHour;

    float Distance = -52.34f;

    float GetSpeed()
    {
        float speed = (((transform.position - lastPosition).magnitude) / Time.deltaTime);
        lastPosition = transform.position;

        return speed;
    }

    void FixedUpdate()
    {
        Speed = GetSpeed();
        Distance += Speed * Time.deltaTime;
        meterPerSecond.text = string.Format("{0:00.00} m", Distance);
        kilometersPerHour.text = string.Format("{0:00.00} km/h", Speed * 3.6f);
        Chim.transform.rotation = Quaternion.Euler(0, 0, 22 - (Speed * 3.6f));
    }

}
