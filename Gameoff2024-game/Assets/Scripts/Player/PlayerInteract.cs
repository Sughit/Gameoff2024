using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public void Interact();
}

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float interactRange = 1.5f;
    [SerializeField] LayerMask interactLayer;

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.E))
        {
            Collider[] col = Physics.OverlapSphere(transform.position, interactRange, interactLayer);
            if(col.Length > 0)
            {
                col[0].TryGetComponent(out IInteractable interactObj);
                interactObj.Interact();
            }
        }
    } 
}
