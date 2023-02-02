using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left_Right : MonoBehaviour
{
    //UD_Floor
    float initPositionX;
    public float distance;
    public float turningPoint;
    //UD_Floor & LR_Floor
    public bool turnSwitch;
    public float moveSpeed;

    void Awake()
    {
        if (gameObject.name == "Ch23_nonPBR@Walk")
        {
            initPositionX = transform.position.x;
            turningPoint = initPositionX - distance;
        }
    }

    void leftRight()
    {
        float currentPositionX = transform.position.x;

        if (currentPositionX >= initPositionX + distance)
        {
            turnSwitch = false;
        }
        else if (currentPositionX <= turningPoint)
        {
            turnSwitch = true;
        }

        if (turnSwitch)
        {
            transform.position = transform.position + new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position = transform.position + new Vector3(-1, 0, 0) * moveSpeed * Time.deltaTime;
        }

    }

    void Update()
    {
        if (gameObject.name == "Ch23_nonPBR@Walk")
        {
            leftRight();
        }

    }
}
