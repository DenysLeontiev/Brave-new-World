using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemInteractable
{
    public void OnInteract(Inventory inventory);
    public void DestroySelf();
}
