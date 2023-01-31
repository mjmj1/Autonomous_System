using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Redcode.Pools;

public class CopyCar : MonoBehaviour
{
    public Camera camera;
    public GameObject Car;
    public GameObject prefab;
    public Text ScriptText;
    public GameObject[] PrefabArraay = new GameObject[3];
    public Transform First;
    public Transform Second;
    public Transform Third;

    public int count;
    Vector3 Car_pos;
    Vector3 Prefab_pos;

    
    [SerializeField]
    public GameObject cameraView;

    [SerializeField]
    public GameObject cameraPos;
    
    [SerializeField]
    public float speed;


    public void Start()
    {

        try
        {
            ScriptText.text = "0";
        }
        catch
        {

        }
        First = GameObject.Find("Add/Del-Btn").transform.Find("1rd Car");
        Second = GameObject.Find("Add/Del-Btn").transform.Find("2rd Car");
        Third = GameObject.Find("Add/Del-Btn").transform.Find("3rd Car");
        First.gameObject.SetActive(false);
        Second.gameObject.SetActive(false);
        Third.gameObject.SetActive(false);
    }

    public void Newobjcet()
    {
        Car_pos = Car.gameObject.transform.position;
        try
        {
            if (PrefabArraay[0] == null)
            {
                First.gameObject.SetActive(true);
                PrefabArraay[0] = Instantiate(prefab, Car_pos + new Vector3(0, 0, -10), Quaternion.identity, transform) as GameObject;
                count++;
                ScriptText.text = count.ToString();
            }
            else if (PrefabArraay[1] == null)
            {
                Second.gameObject.SetActive(true);
                PrefabArraay[1] = Instantiate(prefab, Car_pos + new Vector3(0, 0, -15), Quaternion.identity, transform) as GameObject;
                count++;
                ScriptText.text = count.ToString();
            }
            else if (PrefabArraay[2] == null)
            {
                Third.gameObject.SetActive(true);
                PrefabArraay[2] = Instantiate(prefab, Car_pos + new Vector3(0, 0, -20), Quaternion.identity, transform) as GameObject;
                count++;
                ScriptText.text = count.ToString();
            }
            else if (count == 3)
            {
                GetComponent<Button>().interactable = false;
            }
        }
        catch
        {

        }
    }
    public void DelClone1()
    {
        if (PrefabArraay != null)
        {
            Destroy(PrefabArraay[0]);
            PrefabArraay[0] = null;
            count--;
            ScriptText.text = count.ToString();
            First.gameObject.SetActive(false);
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }
    public void DelClone2()
    {
        if (PrefabArraay != null)
        {
            Destroy(PrefabArraay[1]);
            PrefabArraay[1] = null;
            count--;
            ScriptText.text = count.ToString();
            Second.gameObject.SetActive(false);
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }
    public void DelClone3()
    {
        if (PrefabArraay != null)
        {
            Destroy(PrefabArraay[2]);
            PrefabArraay[2] = null;
            count--;
            ScriptText.text = count.ToString();
            Third.gameObject.SetActive(false);
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }

    public void ChangeView1()
    {
        PrefabArraay[0].AddComponent<Camera>();
        camera.transform.localPosition = camera.transform.localPosition + new Vector3(0f, 1.68f, 3f);
    }

    // public void ChangeView2()
    // {
    //     Prefab_pos = PrefabArraay[1].transform.localPosition;

    //     cameraPos.transform.localPosition = Prefab_pos + new Vector3(0f, 1.68f, 3f);
    //     cameraView.transform.localPosition = Prefab_pos + new Vector3(0f, 1.68f, 3f);
    // }

    // public void ChangeView3()
    // {
    //     Prefab_pos = PrefabArraay[2].transform.localPosition;

    //     cameraPos.transform.localPosition = Prefab_pos + new Vector3(0f, 1.68f, 3f);
    //     cameraView.transform.localPosition = Prefab_pos + new Vector3(0f, 1.68f, 3f);
    // }
}