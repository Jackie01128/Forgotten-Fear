using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public List<itemType> inventoryList;
    public int playerReach;
    [SerializeField] Camera cam;
    [SerializeField] GameObject pressToPickup_gameobject;
    [SerializeField] Image[] inventorySlotImage = new Image[9];
    [SerializeField] Image[] inventoryBackgroundImage = new Image[9];
    [SerializeField] Sprite prazdnySlotImage;
    [SerializeField] GameObject throwObject_gameobject;
    
    [SerializeField] KeyCode throwItemKey;
    [SerializeField] KeyCode pickUpItemKey;

    public int selectedItem = 0;

    //public bool animationIsPlaying = false;
    //dont need the one on top
    [Space(10)]

    [Header("References")]
    public Transform cam1;
    public Transform attackPoint;


    [Header("Throwing")]
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;

    [Header("Zbrane gameobjects")]
    [SerializeField] GameObject flashLight_item;
    [SerializeField] GameObject flare_item;
    [SerializeField] GameObject glowStick_item;
    
    [Header("items prefabs")]
    [SerializeField] GameObject flashLight_prefab;
    [SerializeField] GameObject flare_prefab;
    [SerializeField] GameObject glowStick_prefab;

    private Dictionary<itemType, GameObject> itemSetActive = new Dictionary<itemType, GameObject>() { };
    private Dictionary<itemType, GameObject> itemInstantiate = new Dictionary<itemType, GameObject>() { };

    void Start()
    {
        readyToThrow = true;

        itemSetActive.Add(itemType.FlashLight, flashLight_item);
        itemSetActive.Add(itemType.Flare, flare_item);
        itemSetActive.Add(itemType.GlowStick, glowStick_item);

      
        itemInstantiate.Add(itemType.FlashLight, flashLight_item);
        itemInstantiate.Add(itemType.Flare, flare_prefab);
        itemInstantiate.Add(itemType.GlowStick, glowStick_prefab);
        
        NewItemSelected();
    }


    void Update()
    {
   

        if (Input.GetKeyDown(throwItemKey) && inventoryList[selectedItem] != itemType.FlashLight && readyToThrow && inventoryList.Count > 0)
        {
            Throw(inventoryList[selectedItem]); // Call the modified Throw method

            inventoryList.RemoveAt(selectedItem);

            // Enable the Rigidbody component on the thrown item
            /*gidbody thrownItemRigidbody = itemSetActive[inventoryList[selectedItem]].GetComponent<Rigidbody>();
            if (thrownItemRigidbody != null)
            {
                thrownItemRigidbody.isKinematic = false;
            }
            */
            if (selectedItem != 0)
            {
                selectedItem -= 1;
            }
            NewItemSelected();
        }

        for (int i = 0; i <= 8; i++)
        {
            if (i < inventoryList.Count)
            {
                inventorySlotImage[i].sprite = itemSetActive[inventoryList[i]].GetComponent<Item>().itemScriptableObject.item_sprite;
            }
            else
            {
                inventorySlotImage[i].sprite = prazdnySlotImage;
            }
        }
        
        int a = 0;
        foreach (Image image in inventoryBackgroundImage)
        {
            if (a == selectedItem)
            {
                image.color = new Color32(145, 255, 126, 255);
            }
            else
            {
                image.color = new Color32(219, 219, 219, 255);
            }
            a++;
        }
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;


        if (Physics.Raycast(ray, out hitInfo, playerReach))
        {
            
            IPickable item = hitInfo.collider.GetComponent<IPickable>();
            if (item != null)
            {

                pressToPickup_gameobject.SetActive(true);

                if (Input.GetKey(pickUpItemKey)) {
                    inventoryList.Add(hitInfo.collider.GetComponent<ItemPickable>().itemScriptableObject.item_type);
                    item.PickItem();
                }
                
            }
            else
            {
                pressToPickup_gameobject.SetActive(false);
            }
        }
        else
        {
            pressToPickup_gameobject.SetActive(false);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1) && inventoryList.Count > 0 )
        {
            selectedItem = 0;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && inventoryList.Count > 1 )
        {
            selectedItem = 1;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && inventoryList.Count > 2)
        {
            selectedItem = 2;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && inventoryList.Count > 3 )
        {
            selectedItem = 3;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && inventoryList.Count > 4 )
        {
            selectedItem = 4;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6) && inventoryList.Count > 5)
        {
            selectedItem = 5;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7) && inventoryList.Count > 6 )
        {
            selectedItem = 6;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8) && inventoryList.Count > 7 )
        {
            selectedItem = 7;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9) && inventoryList.Count > 8)
        {
            selectedItem = 8;
            NewItemSelected();
        }
    }

    private void Throw(itemType itemType)
    {
        readyToThrow = false;

        // instantiate object to throw
        GameObject projectile = Instantiate(itemInstantiate[itemType], attackPoint.position, cam1.rotation);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = cam1.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(cam1.position, cam1.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        // add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        // implement throwCooldown
        ResetThrow();
    }


    private void ResetThrow()
    {
        readyToThrow = true;
    }
  
    private void NewItemSelected()
    {
        flashLight_item.SetActive(false);
        flare_item.SetActive(false);
        glowStick_item.SetActive(false);

        //animationIsPlaying = false;
        GameObject selectedItemGameobject = itemSetActive[inventoryList[selectedItem]];
        selectedItemGameobject.SetActive(true);
    }
}


public interface IPickable
{
    void PickItem();

}

