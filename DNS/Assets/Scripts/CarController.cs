using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    private InputAction drive;

    private void OnEnable()
    {
        drive = InputManager.inputActions.Car.Drive;
        InputManager.inputActions.Car.Exit.performed += ExitTractor;
    }

    private void ExitTractor(InputAction.CallbackContext obj)
    {
        InputManager.ToggleActionMap(InputManager.inputActions.Player);
    }
    
    
}
