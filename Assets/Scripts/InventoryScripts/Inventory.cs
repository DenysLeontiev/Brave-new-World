using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class Inventory
{
    public event EventHandler OnItemAdded;

    public List<ItemWorld> itemWorldsList;
    public Inventory()
    {
        itemWorldsList = new List<ItemWorld>();
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

        //itemWorldsList.Add(item);

        //if (item.item.isStackable)
        //{
        //    bool isAlreadyInInvetory = false;
        //    foreach (ItemWorld inventoryItem in itemWorldsList)
        //    {
        //        if (inventoryItem.item.itemType == item.item.itemType)
        //        {
        //            Debug.Log("Just ADD");
        //            inventoryItem.item.amount += item.item.amount;
        //            isAlreadyInInvetory = true;
        //        }
        //    }
        //    if (!isAlreadyInInvetory)
        //    {
        //        Debug.Log("Add first stackable");
        //        itemWorldsList.Add(item);
        //    }
        //}
        //else
        //{
        //    Debug.Log("Add Last");
        //    itemWorldsList.Add(item);
        //}

        //OnItemAdded?.Invoke(null, EventArgs.Empty);
    }

    public void RemoveItem(ItemWorld item)
    {
        Debug.Log(item.item.amount);
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

    public void DropItem(ItemWorld item, Vector3 position, Transform parent)
    {
        
    }

    public List<ItemWorld> GetItems()
    {
        return itemWorldsList;
    }
}
