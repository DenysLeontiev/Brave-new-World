using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask interactionLayer;

    [SerializeField] private Transform overlapBoxCenter;
    [SerializeField] private Vector3 overlapCubeSize = Vector3.one;

    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;

    private void Awake()
    {
        inventory = new Inventory();
    }

    private void Start()
    {
        //inventory = new Inventory();
        uiInventory.SetInventory(inventory);
    }



    private void Update()
    {
        if(Physics.BoxCast(overlapBoxCenter.position, overlapCubeSize, transform.forward, out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent<IObjectInteractable>(out IObjectInteractable interactableObject))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("E pressed");
                    interactableObject.Interact();
                }
            }

            if (hit.transform.TryGetComponent<IItemInteractable>(out IItemInteractable itemInteractable))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //if(canIteract)
                    //{
                    //    Debug.Log("E pressed");
                    //    StartCoroutine(CanInteract(itemInteractable, inventory));
                    //}
                    itemInteractable.OnInteract(inventory);
                }
            }
        }

    }

    bool canIteract = true;
    private IEnumerator CanInteract(IItemInteractable itemInteractable,Inventory inventory)
    {
        canIteract = false;
        itemInteractable.OnInteract(inventory);
        yield return new WaitForSeconds(1);
        canIteract = true;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawCube(overlapBoxCenter.position, overlapCubeSize);
    //}
}
