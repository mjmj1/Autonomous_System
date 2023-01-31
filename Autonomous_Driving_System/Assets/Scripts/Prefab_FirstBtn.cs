using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Redcode.Pools;

public class Prefab_FirstBtn : MonoBehaviour
{
    public CopyCar copycar = new CopyCar();

    [SerializeField]
    public GameObject cameraView;

    // ?????? ???
    [SerializeField]
    public GameObject cameraPos;

    Vector3 Prefab_Pos;

    public void ChangeView()
    {
        copycar = GameObject.Find("CopyCar").GetComponent<CopyCar>();
        GameObject temp = copycar.PrefabArraay[0];

        Prefab_Pos = temp.transform.localPosition;

        cameraPos.transform.localPosition = Prefab_Pos;
        cameraView.transform.localPosition = Prefab_Pos;
    }
}
