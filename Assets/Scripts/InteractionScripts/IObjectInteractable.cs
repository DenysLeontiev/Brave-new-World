using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    MeleeWeapon,
    Food,
    Cloth,
    Object
}

public interface IObjectInteractable
{
    public string ObjectName { get; set; }
    public ObjectType ObjectType { get; set; }

    public void Interact();
}
