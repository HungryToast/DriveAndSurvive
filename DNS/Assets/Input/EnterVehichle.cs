using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnterVehichle : MonoBehaviour
{
    [SerializeField] private Inputs inputsAsset;

    private void Awake()
    {
        inputsAsset = new Inputs();
    }

    private void OnEnable()
    {
        inputsAsset.Player.Use.performed += EnterCar;
        inputsAsset.Car.Exit.performed += ExitCar;
    }
 
    private void ExitCar(InputAction.CallbackContext obj)
    {
        print("Player");
    }

    private void EnterCar(InputAction.CallbackContext obj)
    {
        print("Car");
    }
}
