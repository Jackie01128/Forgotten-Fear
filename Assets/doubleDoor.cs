using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doubleDoor : MonoBehaviour
{
    public Animator door;
    public GameObject openText;

    //public AudioSource doorSound;


    public bool inReach;


    private BoxCollider childBoxCollider;  // Reference to the child BoxCollider

    void Start()
    {
        inReach = false;

        // Get the BoxCollider component from the child object
        childBoxCollider = GetComponentInChildren<BoxCollider>();

        if (childBoxCollider == null)
        {
            Debug.LogError("Child BoxCollider not found!");
        }
    }

    void OnTriggerEnter()
    {

        if (childBoxCollider.gameObject.tag == "Reach")
        {
            inReach = true;
            openText.SetActive(true);
        }
    }

    void OnTriggerExit()
    {
        if (childBoxCollider.gameObject.tag == "Reach")
        {

            inReach = false;
            openText.SetActive(false);

        }
    }





    void Update()
    {

        if (inReach && Input.GetButtonDown("Interact"))
        {
            DoorOpens();
            Debug.Log("Door has opened");
        }

        else
        {
            DoorCloses();
            Debug.Log("Door has opened");
        }




    }
    void DoorOpens()
    {
        //Debug.Log("It Opens");
        door.SetBool("Open", true);
        door.SetBool("Closed", false);
        //doorSound.Play();

    }

    void DoorCloses()
    {
        //Debug.Log("It Closes");
        door.SetBool("Open", false);
        door.SetBool("Closed", true);
    }


}