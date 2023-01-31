using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnClick : MonoBehaviour
{
    [SerializeField]
    private Transform CamPos;

    [SerializeField]
    private Transform CamView;

    public void BackClick()
    {
        CamPos.localPosition = new Vector3(0f, 3f, -6f);
        CamView.localPosition = new Vector3(0f, 1.7f, 3f);
    }
    public void FrontClick()
    {
        CamPos.localPosition = new Vector3(0f, 3f, 6f);
        CamView.localPosition = new Vector3(0f, 1.7f, 0f);
    }
    public void SideClick()
    {
        CamPos.localPosition = new Vector3(10f, 3f, 0f);
        CamView.localPosition = new Vector3(0f, 1.7f, 0f);
    }

    public void PointViewlick()
    {
        CamPos.localPosition = new Vector3(0f, 2f, 1f);
        CamView.localPosition = new Vector3(0f, 2f, 6f);
    }

    

    
}
