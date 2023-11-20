using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSwitching : MonoBehaviour
{
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
            SwitchItem(0);
        }
        else if (Input.GetKeyDown("2"))
        {
            SwitchItem(1);
        }
        else if (Input.GetKeyDown("3"))
        {
            SwitchItem(2);
        }

        // Use the mouse scroll wheel to switch items
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel > 0f)
        {
            // Scroll up
            SwitchItem((currentItemIndex + 1) % (itemsToThrow.Length + 1));
        }
        else if (scrollWheel < 0f)
        {
            // Scroll down
            SwitchItem((currentItemIndex - 1 + itemsToThrow.Length + 1) % (itemsToThrow.Length + 1));
        }

        // Throwing logic
        if (Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0)
        {
            Throw();
        }

        // Toggle flashlight
        if (Input.GetKeyDown(KeyCode.F) && currentItemIndex == itemsToThrow.Length)
        {
            ToggleFlashlight();
        }
    }

    void SwitchItem(int index)
    {
        currentItemIndex = index;
        SetActiveItem(currentItemIndex);
    }

    void SetActiveItem(int index)
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

    void Throw()
    {
        readyToThrow = false;

        // instantiate object to throw
        GameObject projectile = Instantiate(itemsToThrow[currentItemIndex], transform.position, transform.rotation);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = transform.forward;

        // add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        // implement throwCooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    void ToggleFlashlight()
    {
        // Implement your logic to toggle the flashlight (if flashlight is the last item in the array)
        Debug.Log("Toggling flashlight...");
    }

    void ResetThrow()
    {
        readyToThrow = true;
    }
}
