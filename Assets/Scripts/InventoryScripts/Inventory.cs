using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class Inventory
{
    public event EventHandler OnItemAdded;
    private Action<ItemWorld> OnItemUse;

    public List<ItemWorld> itemWorldsList;
    public Inventory(Action<ItemWorld> OnItemUse)
    {
        itemWorldsList = new List<ItemWorld>();
        this.OnItemUse = OnItemUse;
    }

    public void AddItem(ItemWorld item)
    {
        if (itemWorldsList.Contains(item)) // to avoid adding multiple objects at one click
        {
            return;
        }

        if (item.item.isStackable)
        {
            bool isAlreadyInInvetory = false;
            foreach (ItemWorld inventoryItem in itemWorldsList)
            {
                if (inventoryItem.item.itemType == item.item.itemType)
                {
                    inventoryItem.item.amount += item.item.amount;
                    isAlreadyInInvetory = true;
                }
            }
            if (!isAlreadyInInvetory)
            {
                itemWorldsList.Add(item);
            }
        }
        else
        {
            if (!itemWorldsList.Contains(item))
            {
                itemWorldsList.Add(item);
            }
        }

        OnItemAdded?.Invoke(null, EventArgs.Empty);
    }

    public void RemoveItem(ItemWorld item)
    {
        if (item.item.isStackable)
        {
            Item itemInInvetory = null;
            foreach (ItemWorld inventoryItem in itemWorldsList)
            {
                if (inventoryItem.item.itemType == item.item.itemType)
                {
                    inventoryItem.item.amount -= 1;
                    itemInInvetory = inventoryItem.item;
                }
            }
            if (itemInInvetory != null && itemInInvetory.amount <= 0)
            {
                itemWorldsList.Remove(item);
            }
        }
        else
        {
            itemWorldsList.Remove(item);
        }

        OnItemAdded?.Invoke(null, EventArgs.Empty);
    }

    public void UseItem(ItemWorld itemWorld)
    {
        if (PlayerInteraction.canEquipMelee) // if WeaponPosition is not empty,so we dont remove from inventory 
        {
            RemoveItem(itemWorld);
            OnItemUse(itemWorld);
        }
    }

    public List<ItemWorld> GetItems()
    {
        return itemWorldsList;
    }
}
