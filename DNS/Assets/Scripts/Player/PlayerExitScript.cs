using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerExitScript : MonoBehaviour
{
    [SerializeField] Inputs inputAsset;
    [SerializeField] bool atExit, canExit;
    [SerializeField] private Canvas _endScreen, playerUI;

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
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                playerUI.gameObject.SetActive(false);
                _endScreen.gameObject.SetActive(true);
            }
        }
    }

    public void AtExit(bool state)
    {
        atExit = state;
    }

    public void CanExit(bool state)
    {
        canExit = state;
    }
}
