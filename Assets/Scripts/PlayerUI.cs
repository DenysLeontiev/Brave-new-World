using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject playerInventoryUI;
    [SerializeField] private KeyCode activateButton;
    private bool isInInventory = false;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        HandleInventory();
    }

    private void HandleInventory()
    {
        if(Input.GetKeyDown(activateButton) && isInInventory == false)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            playerInventoryUI.SetActive(true);
            isInInventory = true;
        } else if(Input.GetKeyDown(activateButton) && isInInventory == true)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            playerInventoryUI.SetActive(false);
            isInInventory = false;
        }
    }
}
