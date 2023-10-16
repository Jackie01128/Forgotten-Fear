using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpDrop : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask PickUpLayerMask;

    private void Update(){
        if (Input.GetKeyDown(KeyCode.E){
            float pickUpDistance = 2f;
            if (Physics.Raycast(playerCameraTransform.position,playerCameraTransform.forward,out RaycastHit raycastHit, PickUpLayerMask))
                debug.Log(raycastHit.transform);
        }
    }
}
