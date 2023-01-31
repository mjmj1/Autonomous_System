using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCamFollwing : MonoBehaviour
{
    [SerializeField]
    public GameObject cameraView;

    // ?????? ???
    [SerializeField]
    public GameObject cameraPos;

    [SerializeField]
    public float speed;

    public void updat()
    {
        cameraPos.transform.localPosition = new Vector3(23f, 7f, -3f);
        cameraView.transform.localPosition = new Vector3(0f, 1.68f, -5f);
    }
}
