using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpUv : MonoBehaviour
{
    
    public GameObject UvOnPlayer;
    void Start()
    {
        UvOnPlayer.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(Input.GetKey(KeyCode.E))
            {
                this.gameObject.SetActive(false);
                UvOnPlayer.SetActive(true);
            }
        }
    }
}
