using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTest : MonoBehaviour, IObjectInteractable
{
    [SerializeField] private string name;

    public string ObjectName
    {
        get { return name; }
        set { name = value; }
    }


    public void Interact(Transform grabPosition)
    {
        Debug.Log(ObjectName);
    }
}
