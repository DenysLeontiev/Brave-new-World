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

    //private void Update()
    //{
    //    transform.localPosition = swordPosition;
    //    transform.localRotation = Quaternion.Euler(swordRotation);
    //}

    public void Interact()
    {
        Debug.Log(ObjectName);
        Debug.Log(ObjectType);
        if(weaponPosition.childCount <= 0)
        {
            float speed = 2f;
            transform.parent = weaponPosition;
            //transform.localPosition = Vector3.Lerp(transform.position, new Vector3(-0.16f, 0.12f, -0.06f), speed * Time.deltaTime);
            transform.localPosition = new Vector3(-0.16f, 0.12f, -0.06f);
            transform.localRotation = Quaternion.Euler(new Vector3(117.4f, 603.9f, -27.9f));
            //transform.Rotate(swordRotation);
        }
        else
        {
            Debug.LogError("Weapon Position is not empty");
        }
    }
}
