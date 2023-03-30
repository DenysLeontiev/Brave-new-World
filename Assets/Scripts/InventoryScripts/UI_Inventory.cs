using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UI_Inventory : MonoBehaviour
{
    [SerializeField] private Transform spawnObjectTransform;

    private Transform player;

    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private Transform emptySlotTemplate;
    private Transform emtySlotParent;

    private void Start()
    {
        player = FindObjectOfType<PlayerInteraction>().GetComponent<Transform>();
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        //emtySlotParent = itemSlotContainer.Find("emtySlotParent");
        emtySlotParent = transform.Find("emtySlotParent");
        emptySlotTemplate = emtySlotParent.Find("emptySlotTemplate");

        RefreshEmptyInventorySlots();
    }

    private void Update()
    {
        RefreshEmptyInventorySlots();
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemAdded += Inventory_OnItemAdded;

        RefreshInventory();
    }

    private void Inventory_OnItemAdded(object sender, System.EventArgs e)
    {
        RefreshInventory();
        RefreshEmptyInventorySlots();
    }

    private void RefreshInventory()
    {
        if (itemSlotContainer == null) return;
        foreach (Transform child in itemSlotContainer)
        {
            if (child.parent == emptySlotTemplate) continue;
            if(child == itemSlotTemplate)
            {
                continue;
            }
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float cellSize = 100f;
        foreach (ItemWorld itemWorld in inventory.GetItems())
        {
            RectTransform itemTemplateRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemTemplateRectTransform.gameObject.SetActive(true);

            //itemTemplateRectTransform.GetComponent<Button_UI>().ClickFunc = () => // Left Mouse Click Func
            //{
            //    inventory.UseItem(itemWorld);
            //};

            itemTemplateRectTransform.GetComponent<Button_UI>().MouseMiddleClickFunc = () =>
            {
                inventory.UseItem(itemWorld); ;
            };

            itemTemplateRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () => // Right Mouse Click Func
            {
                //Debug.Log("Remove item");
                DropItem(itemWorld, player);
                inventory.RemoveItem(itemWorld);
            };

            itemTemplateRectTransform.anchoredPosition = new Vector2(x * cellSize, -y * cellSize);

            Image templateImage = itemTemplateRectTransform.Find("image").GetComponent<Image>();
            templateImage.sprite = itemWorld.GetSprite();

            TextMeshProUGUI amountText = itemTemplateRectTransform.Find("amount").GetComponent<TextMeshProUGUI>();
            if(itemWorld.item.amount > 1)
            {
                amountText.SetText(itemWorld.item.amount.ToString());
            }
            else
            {
                amountText.SetText("");
            }

            //Debug.Log("iTEM AMOUNT: " + item.item.amount);

            x++;
            if(x > 4)
            {
                x = 0;
                y++;
            }
        }
    }

    private void DropItem(ItemWorld item, Transform player)
    {
        switch (item.item.itemType)
        {
            case Item.ItemType.Sword:
                SpawnObject(ItemAssests.Instance.WeakSwordPrefab, item, player);
                break;
            case Item.ItemType.Apple:
                SpawnObject(ItemAssests.Instance.ApplePrefab, item, player);
                break;
            case Item.ItemType.Wood:
                SpawnObject(ItemAssests.Instance.WoodPrefab, item, player);
                //TODO: Finish implemetation of Dropping items out of inventories
                break;
            case Item.ItemType.Object:
                break;
            default:
                break;
        }
    }

    List<Transform> emptySlots = new List<Transform>();
    private void RefreshEmptyInventorySlots()
    {
        int rowX = 5;
        int columnY = 3;
        float cellSize = 100f;

        if(emptySlots.Count >= rowX * columnY)
        {
            return;
        }

        for (int x = 0; x < rowX; x++)
        {
            for (int y = 0; y < columnY; y++)
            {
                var emptySlot = Instantiate(emptySlotTemplate, emtySlotParent);
                emptySlots.Add(emptySlot);  
                emptySlot.gameObject.SetActive(true);
                emptySlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(x * cellSize, -y * cellSize);
            }
        }
    }

    private void SpawnObject(GameObject objectPrefab, ItemWorld item, Transform player)
    {
        float xOffset = 0f;
        float yOffset = 0f;
        float zOffset = 0f;
        Vector3 spawnPos = new Vector3(player.transform.position.x + xOffset, player.transform.position.y
            + yOffset, player.transform.forward.z + player.transform.position.z + zOffset);

        GameObject spawnedItem = Instantiate(objectPrefab, spawnPos, Quaternion.identity);
        spawnedItem.AddComponent<Rigidbody>();
        //Rigidbody spawnedObjectRigidbody = spawnedItem.GetComponent<Rigidbody>();
        //spawnedObjectRigidbody.AddForce(new Vector3(0, 0, 0f));
    }
}
