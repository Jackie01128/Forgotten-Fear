using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpKey : MonoBehaviour
{
    public GameObject mapOB;
    public GameObject invOB;
    public GameObject pickUpText;
    public AudioSource objSound;

    public bool inReach;

    public bool playerHasKey;


    void Start()
    {
        inReach = false;
        pickUpText.SetActive(false);
        invOB.SetActive(false);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            pickUpText.SetActive(true);

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            pickUpText.SetActive(false);

        }
    }


    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact"))
        {
            mapOB.SetActive(false);
            objSound.Play();
            invOB.SetActive(true);
            pickUpText.SetActive(false);
            /*playerHasKey = true;
            int intValue = playerHasKey ? 1 : 0;
            PlayerPrefs.SetInt("playerHasKey", intValue);
            PlayerPrefs.Save();
            */
        }

        
    }
}
