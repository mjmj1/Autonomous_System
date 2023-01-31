using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpCamFollwing : MonoBehaviour
{
    [SerializeField]
    public GameObject cameraView;

    // ī�޶��� ��ġ
    [SerializeField]
    public GameObject cameraPos;

    [SerializeField]
    public float speed;

    public void updat()
    {
        cameraPos.transform.localPosition = new Vector3(0f, 12f, -7f);
        cameraView.transform.localPosition = new Vector3(0f, 1.68f, 3f);
    }
}
