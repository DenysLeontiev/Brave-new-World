using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Item;

public class ItemWorld : MonoBehaviour, IItemInteractable
{
    public Item item;

    public Sprite GetSprite()
    {
        switch (item.itemType)
        {
            default:
            case ItemType.Sword:  return ItemAssests.Instance.swordSprite;
            case ItemType.Apple:  return ItemAssests.Instance.appleSprite;
            case ItemType.Wood:   return ItemAssests.Instance.woodSprite;
            case ItemType.Object: return ItemAssests.Instance.objectSprite;
        }

    }

    public void OnInteract(Inventory inventory)
    {
        inventory.AddItem(this);
        DestroySelf();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
