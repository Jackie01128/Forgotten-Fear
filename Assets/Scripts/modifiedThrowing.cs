using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ModifiedThrowing : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject[] itemsToThrow;
    private int currentItemIndex = 0;

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;

    private bool readyToThrow;

    void Start()
    {
        readyToThrow = true;
        SetActiveItem(currentItemIndex);
    }

    void Update()
    {
        // Switching between items and no item
        if (Input.GetKeyDown("1"))
        {
            currentItemIndex = (currentItemIndex + 1) % (itemsToThrow.Length + 1);
            SetActiveItem(currentItemIndex);
        }

        // Throwing logic
        if (Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0)
        {
            Throw();
        }
    }

    private void SetActiveItem(int index)
    {
        // Deactivate all items
        foreach (GameObject item in itemsToThrow)
        {
            if (item != null)
            {
                item.SetActive(false);
            }
        }

        // Activate the selected item or set to null for "no item"
        if (index < itemsToThrow.Length)
        {
            itemsToThrow[index].SetActive(true);
        }
    }

    private void Throw()
    {
        readyToThrow = false;

        // instantiate object to throw
        GameObject projectile = Instantiate(itemsToThrow[currentItemIndex], attackPoint.position, cam.rotation);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        // add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        // implement throwCooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}