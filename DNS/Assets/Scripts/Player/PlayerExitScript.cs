using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerExitScript : MonoBehaviour
{
    [SerializeField] Inputs inputAsset;
    [SerializeField] bool atExit, canExit;

    private void Awake()
    {
        inputAsset = new Inputs();
        atExit = false;
        canExit = false;
    }

    private void OnEnable()
    {
        inputAsset.Player.Use.started += Exit;
        inputAsset.Player.Enable();
        inputAsset.Car.Disable();
    }

    private void Exit(InputAction.CallbackContext obj)
    {
        if (atExit)
        {
            if (canExit)
            {
                print("Game Over");
            }
            else if(!canExit)
            {
                print("You dont have the necessary materials to leave");
            }
        }
    }

    public void AtExit(bool state)
    {
        atExit = state;
        print("player at exit");
    }

    public void CanExit(bool state)
    {
        canExit = state;
        print("player can exit");
    }
}
