using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectInteractable
{
    public string ObjectName { get; set; }

    public void Interact(Transform grabPosition);
}
