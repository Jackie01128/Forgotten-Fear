using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFollow : MonoBehaviour
{
    public Transform target; // The target to follow (camera in this case)
    public Vector3 offset = new Vector3(0f, 0.5f, 1f); // Offset from the target

    void Update()
    {
        // Make sure the target is assigned
        if (target != null)
        {
            // Update the position and rotation of the object
            transform.position = target.position + target.TransformDirection(offset);
            transform.rotation = target.rotation;
        }
    }
}
