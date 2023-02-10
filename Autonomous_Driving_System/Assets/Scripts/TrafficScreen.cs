using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficScreen : MonoBehaviour
{
    [SerializeField] GameObject[] Red;
    [SerializeField] GameObject[] Orange;
    [SerializeField] GameObject[] Green;
    [SerializeField] GameObject[] Person;
   
    public void Update()
    {
        string A = GameObject.Find("Car").GetComponent<DrivingAgent>().serverMsg;
        
 

        if(A.Contains("green_light"))
        {
            for(int i = 0; i < Red.Length; i++)
            {              
                Red[i].gameObject.SetActive(true);
                Green[i].gameObject.SetActive(false);
            }
            for(int i = 0; i < Person.Length; i++)
            {
                Person[i].gameObject.SetActive(false);
            }

        }
        else if(A.Contains("red_light"))
        {
            for (int i = 0; i < Red.Length; i++)
            {
                Red[i].gameObject.SetActive(false);
                Green[i].gameObject.SetActive(true);
            }
            for (int i = 0; i < Person.Length; i++)
            {
                Person[i].gameObject.SetActive(false);
            }

        } 
        else if(A.Contains("people"))
        {
            for (int i = 0; i < Red.Length; i++)
            {
                Red[i].gameObject.SetActive(false);
                Green[i].gameObject.SetActive(true);
            }
            for (int i = 0; i < Person.Length; i++)
            {
                Person[i].gameObject.SetActive(true);
            }
        }
    }
}
