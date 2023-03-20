using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType
    {
        Sword,
        Apple, 
        Wood,
        Object
    }

    public Sprite itemSprite;
    public ItemType itemType;
    public int amount;
    public bool isStackable;

    //public Sprite GetSprite()
    //{
    //    switch (itemType)
    //    {
    //        default:
    //        case ItemType.Sword: return ItemAssests.Instance.swordSprite;
    //        case ItemType.Apple: return ItemAssests.Instance.appleSprite;
    //        case ItemType.Wood: return ItemAssests.Instance.swordSprite;
    //        case ItemType.Object: return ItemAssests.Instance.objectSprite;
    //    }

    //}
}
