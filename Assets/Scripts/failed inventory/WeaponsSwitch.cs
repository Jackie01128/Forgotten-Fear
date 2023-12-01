using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsSwitch : MonoBehaviour
{
    public GameObject object01;
    public GameObject object02;
    public GameObject object03;
    public GameObject object04;
    //public GameObject object05;

    private void Start()
    {
        DeactivateAllObjects();
    }

    private void DeactivateAllObjects()
    {
        object01.SetActive(false);
        object02.SetActive(false);
        object03.SetActive(false);
        object04.SetActive(false);
 

        //object05.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("1"))
        {
            
        }

        if (Input.GetButtonDown("2"))
        {
            
        }

        if (Input.GetButtonDown("3"))
        {
            ToggleObject(object03);
        }

        if (Input.GetButtonDown("4"))
        {
            
            if (PlayerPrefs.HasKey("playerHasKey"))
            {
                int value = PlayerPrefs.GetInt("playerHasKey");

                if (value == 1)
                {
                    DeactivateAllObjects(); // Comment this line if you want to allow multiple objects to be active simultaneously
                    object04.SetActive(true);
                    Debug.Log("Hello");
                }
                else {
                    object04.SetActive(false);
                }
                    
         
                PlayerPrefs.DeleteKey("playerHasKey");
            }
            
        }

        /*
        if (Input.GetButtonDown("5"))
        {
            ToggleObject(object05);
        }
        */
    }

    private void ToggleObject(GameObject obj)
    {
        if (obj.activeSelf)
        {
            Debug.Log(obj.activeSelf);
            obj.SetActive(false);
        }
        else
        {
            DeactivateAllObjects(); // Comment this line if you want to allow multiple objects to be active simultaneously
            obj.SetActive(true);
        }
    }
}
