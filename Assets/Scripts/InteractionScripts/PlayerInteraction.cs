using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask interactionLayer;

    [SerializeField] private Transform overlapBoxCenter;
    [SerializeField] private Vector3 overlapCubeSize = Vector3.one;

    void Start()
    {
        
    }

    void Update()
    {
        if(Physics.BoxCast(overlapBoxCenter.position, overlapCubeSize, transform.forward, out RaycastHit hit))
        {
            if(hit.transform.TryGetComponent<IObjectInteractable>(out IObjectInteractable interactableObject))
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    interactableObject.Interact(overlapBoxCenter);
                }
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(overlapBoxCenter.position, overlapCubeSize);
    }
}
