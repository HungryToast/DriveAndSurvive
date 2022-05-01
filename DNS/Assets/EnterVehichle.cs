using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnterVehichle : MonoBehaviour
{
    [SerializeField] private Inputs inputsAsset = new Inputs();
    

    private void OnEnable()
    {
        inputsAsset.Player.Use.started += EnterCar;
    }

    private void EnterCar(InputAction.CallbackContext obj)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.up * 0.25f, Vector3.forward);
        if (Physics.Raycast(ray,out hit, 3f))
        {
            if(hit.transform.gameObject.CompareTag("Vehicle")) hit.transform.gameObject.GetComponent<InputManager>().ToggleActionMap(inputsAsset.Car);
        }
    }
}
