using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontCamFollwing : MonoBehaviour
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
        cameraPos.transform.localPosition = new Vector3(0f, 2f, 6f);
        cameraView.transform.localPosition = new Vector3(0f, 1.68f, 3f);
    }
}
