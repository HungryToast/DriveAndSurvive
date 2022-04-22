using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Camera thirdPersonCamera;
    public float interactDistance;
    public Interactables objectHit;
    public LayerMask hitThis;
    

    private void Awake()
    {
        thirdPersonCamera = Camera.main;
        
    }

    private bool IsInteracable() 
    {
        RaycastHit hit;

        if (Physics.Raycast(thirdPersonCamera.transform.position, thirdPersonCamera.transform.TransformDirection(Vector3.forward), out hit, interactDistance,hitThis)){
            objectHit = hit.collider.GetComponent<Interactables>();
            print(hit.collider);
            return true;
        }
        else {
            return false;
        }
    }

    private void OnInteract() 
    {
        if (IsInteracable()){
            objectHit.InvokeInteraction();
        }

    }

}
