using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UI_Inventory : MonoBehaviour
{
    private Transform player;

    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    private void Start()
    {
        player = FindObjectOfType<PlayerInteraction>().GetComponent<Transform>();
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
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
    }

    private void RefreshInventory()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if(child == itemSlotTemplate)
            {
                continue;
            }
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float cellSize = 110f;
        foreach (ItemWorld item in inventory.GetItems())
        {
            Debug.Log("Test item: " + item);
            RectTransform itemTemplateRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemTemplateRectTransform.gameObject.SetActive(true);

            itemTemplateRectTransform.GetComponent<Button_UI>().ClickFunc = () => // Left Mouse Click Func
            {
                Debug.Log("Use item");
            };

            itemTemplateRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () => // Right Mouse Click Func
            {
                //Debug.Log("Remove item");
                DropItem(item, player);
                inventory.RemoveItem(item);
            };

            itemTemplateRectTransform.anchoredPosition = new Vector2(x * cellSize, -y * cellSize);

            Image templateImage = itemTemplateRectTransform.Find("image").GetComponent<Image>();
            templateImage.sprite = item.GetSprite();

            TextMeshProUGUI amountText = itemTemplateRectTransform.Find("amount").GetComponent<TextMeshProUGUI>();
            if(item.item.amount > 1)
            {
                amountText.SetText(item.item.amount.ToString());
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
        float xOffset = 5f;
        float yOffset = 6f;
        float zOffset = 2f;
        Vector3 spawnPos = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, transform.position.z + zOffset);

        switch (item.item.itemType)
        {
            case Item.ItemType.Sword:
                SpawnObject(ItemAssests.Instance.WeakSwordPrefab, item, player);
                break;
            case Item.ItemType.Apple:
                SpawnObject(ItemAssests.Instance.ApplePrefab, item, player);
                break;
            case Item.ItemType.Wood:
                //TODO: Finish implemetation of Dropping items out of inventories
                break;
            case Item.ItemType.Object:
                break;
            default:
                break;
        }
    }

    private void SpawnObject(GameObject objectPrefab, ItemWorld item, Transform player)
    {
        GameObject spawnedItem = Instantiate(objectPrefab, player.position, Quaternion.identity);
        spawnedItem.AddComponent<Rigidbody>();
    }
}
