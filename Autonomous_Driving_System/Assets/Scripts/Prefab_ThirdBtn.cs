//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Prefab_ThirdBtn : MonoBehaviour
//{
//    public CopyCar copycar = new CopyCar();

//    [SerializeField]
//    public GameObject cameraView;

//    // ?????? ???
//    [SerializeField]
//    public GameObject cameraPos;

//    Vector3 Prefab_Pos;

//    public void ChangeView()
//    {
//        copycar = GameObject.Find("CopyCar").GetComponent<CopyCar>();
//        GameObject temp = copycar.PrefabList[2];

//        Prefab_Pos = temp.transform.position;

//        cameraPos.transform.localPosition = Prefab_Pos;
//        cameraView.transform.localPosition = Prefab_Pos;
//    }

//    public void DelClone()
//    {
//        try
//        {
//            copycar = GameObject.Find("CopyCar").GetComponent<CopyCar>();

//            Destroy(copycar.PrefabList[2]);
//            copycar.PrefabList.RemoveAt(2);
//        }
//        catch
//        {

//        }
//    }
//}
