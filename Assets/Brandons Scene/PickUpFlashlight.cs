using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpFlashlight : MonoBehaviour
{
    
    public GameObject FlashLightOnPlayer;
    void Start()
    {
        FlashLightOnPlayer.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(Input.GetKey(KeyCode.E))
            {
            this.gameObject.SetActive(false);
            FlashLightOnPlayer.SetActive(true);
            }
        }
    }
}
