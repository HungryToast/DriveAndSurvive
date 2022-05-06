using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    private Inputs inputAsset;
    private InputAction drive, brake;

    [SerializeField] WheelCollider[] _wheelColliders;
    [SerializeField] private GameObject wheelShape, playerCamera,carCamera;
    [SerializeField] private PlayerController _playerController;

    [SerializeField] float torque, maxTorque, maxSteeringAngle, criticalSpeed, steerAngle, breakPower;
    [SerializeField] private int stepBelow, stepAbove;
    
    [SerializeField] private GameObject wheelMesh;

    

    private void ExitCar(InputAction.CallbackContext obj)
    {
        carCamera.SetActive(false);
        playerCamera.SetActive(true);
        inputAsset.Car.Disable();
        _playerController.EnablePlayerControl();
        GetComponent<PlayerExitandEnterCar>().ToggleEnterExit();
        GetComponent<PlayerExitandEnterCar>().SpawnPlayerNextToCar();
    }

    public void EnableControl()
    {
        inputAsset.Car.Enable();
        drive = inputAsset.Car.Drive;
        brake = inputAsset.Car.Brake;
        wheelMesh.SetActive(false);
        inputAsset.Car.Exit.started += ExitCar;
    }

    private void Start()
    {
        _wheelColliders = GetComponentsInChildren<WheelCollider>();
        for (int i = 0; i < _wheelColliders.Length; i++)
        {
            var wheel = _wheelColliders[i];
            if (wheel != null)
            {
                var ws = Instantiate(wheelShape);
                ws.transform.parent = wheel.transform;
            }
        }

        inputAsset = new Inputs();
    }

    public void StopCar()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        foreach (WheelCollider wheel in _wheelColliders)
        {
            wheel.motorTorque = 0;
            wheel.steerAngle = 0;
        }
        torque = 0;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    private void FixedUpdate()
    {
        if (inputAsset.Car.enabled)
        {
                if (drive.IsPressed())
                {
                    if (drive.ReadValue<Vector2>().y >0 && torque < maxTorque)
                    {
                        torque += drive.ReadValue<Vector2>().y * maxTorque;
                    }
                    else if (drive.ReadValue<Vector2>().y < 0 && torque > -maxTorque)
                    {
                        torque += drive.ReadValue<Vector2>().y * maxTorque;
                    }
                    
                    steerAngle = drive.ReadValue<Vector2>().x * maxSteeringAngle;
                }

                if (brake.IsPressed())
                {
                   //GetComponent<Rigidbody>().velocity = Vector3.zero;
                   torque = 0;
                }
                    
                _wheelColliders[0].ConfigureVehicleSubsteps(criticalSpeed, stepBelow, stepAbove);

                foreach (WheelCollider wheel in _wheelColliders)
                {
                    if (wheel.transform.localPosition.z > 0)
                    {
                        wheel.steerAngle = steerAngle;
                    }

                    if (wheel.transform.localPosition.z < 0)
                    {
                        wheel.brakeTorque = breakPower;
                    }

                    wheel.motorTorque = torque;

                    if (wheelShape)
                    {
                        Quaternion q;
                        Vector3 p;
                        wheel.GetWorldPose(out p, out q);
                        Transform shapeTransform = wheel.transform.GetChild(0);

                        if ( wheel.name == "frontRight" || wheel.name == "backRight")
                        {
                            shapeTransform.rotation = q * Quaternion.Euler(0,-180,0);
                            shapeTransform.position = p;
                        }
                        else
                        {
                            shapeTransform.position = p;
                            shapeTransform.rotation = q;
                        } 
                    } 
                } 
        } 
    }
}
