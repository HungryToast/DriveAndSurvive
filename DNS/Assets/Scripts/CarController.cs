using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    private InputAction drive;
    [SerializeField]private WheelCollider[] _wheelColliders;
    [SerializeField] private float driveForce;
    [SerializeField] private float brakeForce;
    [SerializeField] private bool parking;

    private void OnEnable()
    {
        //input map change when exiting vehichle
        drive = InputManager.inputActions.Car.Drive;
        InputManager.inputActions.Car.Exit.performed += ExitCar;
        
        
    }

    private void FixedUpdate()
    {
        
        
        if (drive.ReadValue<Vector2>().sqrMagnitude > 0.1f )
        {
            for (int i = 0; i < _wheelColliders.Length; i++)
            {
                _wheelColliders[i].motorTorque = driveForce;
            }
        }
        

        
    }

    
    private void ExitCar(InputAction.CallbackContext obj)
    {
        InputManager.ToggleActionMap(InputManager.inputActions.Player);
    }

}
