using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordInteractable : MonoBehaviour, IObjectInteractable
{
    [SerializeField] private Transform weaponPosition;

    [SerializeField] private Vector3 swordPosition;
    [SerializeField] private Vector3 swordRotation;

    [SerializeField] private string interactableName;
    public string ObjectName 
    {
        get => interactableName;
        set => interactableName = value; 
    }

    [SerializeField] private ObjectType interactableType;
    public ObjectType ObjectType 
    {
        get => interactableType;
        set => interactableType = value;
    }

    public void Interact()
    {
        Debug.Log("Sword Interaction Player");
        if(weaponPosition.childCount <= 0)
        {
            transform.parent = weaponPosition;
            transform.localPosition = new Vector3(-0.16f, 0.12f, -0.06f);
            transform.localRotation = Quaternion.Euler(new Vector3(117.4f, 603.9f, -27.9f));
        }
        else
        {
            Debug.LogError("Weapon Position is not empty");
        }
    }
}
