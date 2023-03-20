using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left");
        }
        else if(Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right");
        }
    }
}
