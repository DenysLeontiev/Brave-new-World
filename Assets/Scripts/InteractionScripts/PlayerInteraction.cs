using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static bool canEquipMelee = true;

    [SerializeField] private Transform weaponPosition;

    [SerializeField] private LayerMask interactionLayer;

    [SerializeField] private Transform overlapBoxCenter;
    [SerializeField] private Vector3 overlapCubeSize = Vector3.one;

    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;

    [SerializeField] private TextMeshProUGUI interactionText;


    private void Awake()
    {
        inventory = new Inventory(UseItem);
    }

    private void Start()
    {
        //inventory = new Inventory();
        uiInventory.SetInventory(inventory);
    }

    private void Update()
    {
        DetectInteraction();
        RemoveEquippedMelee();
    }

    private void UseItem(ItemWorld item)
    {
        switch (item.item.itemType)
        {
            case Item.ItemType.Sword:
                if(weaponPosition.childCount == 0)
                {
                    AddMeleeInArm();
                    canEquipMelee = false;
                }
                Debug.Log("Sword");
                break;
            case Item.ItemType.Apple:
                Debug.Log("MMM...Yummy Apple");
                break;
            case Item.ItemType.Wood:
                Debug.Log("Wood");
                break;
        }
    }

    private void AddMeleeInArm()
    {
        if (weaponPosition.childCount <= 0)
        {
            var swordPrefab = Instantiate(ItemAssests.Instance.WeakSwordPrefab, weaponPosition);
            swordPrefab.transform.localPosition = new Vector3(-0.16f, 0.12f, -0.06f);
            swordPrefab.transform.localRotation = Quaternion.Euler(new Vector3(117.4f, 603.9f, -27.9f));
        }
        else
        {
            Debug.LogError("Weapon Position is not empty");
        }
    }

    private void RemoveEquippedMelee()
    {
        ItemWorld equippedObject = weaponPosition.GetComponentInChildren<ItemWorld>();
        if(equippedObject != null && Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Un equip melee weapon");
            inventory.AddItem(equippedObject);
            Destroy(equippedObject.gameObject);
            canEquipMelee = true; // used to indicate whether we can equip melee or not(if true,so we can,otherwise - no)
        }
    }

    private void DetectInteraction()
    {
        float maxDistance = overlapCubeSize.x / 2;
        bool isHit = Physics.BoxCast(overlapBoxCenter.position, overlapCubeSize, transform.forward, out RaycastHit hit, Quaternion.identity, maxDistance);
        if (isHit)
        {
            if (hit.transform.TryGetComponent<ItemWorld>(out ItemWorld itewWorld))
            {
                DisplayInteractionText(itewWorld);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    itewWorld.OnInteract(inventory);
                }
            }
            else
            {
                DisplayInteractionText(null);
            }

        }
    }

    private void DisplayInteractionText(ItemWorld itemWorld)
    {
        ItemWorld currentItewWorld = null;
        if(itemWorld != null)
        {
            interactionText.SetText("[E]\n" + itemWorld.item.itemType);
            currentItewWorld = itemWorld;
        }
        else if(currentItewWorld != itemWorld || itemWorld == null)
        {
            interactionText.SetText("");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(overlapBoxCenter.position, overlapCubeSize);
    }
}
