using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMapChange : MonoBehaviour
{
    public static Inputs inputActions;

    public static event Action<InputActionMap> actionMapChange;

    private void Awake()
    {
        inputActions = new Inputs();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void Start()
    {
        ToggleActionMap(inputActions.Player);
    }

    public static void ToggleActionMap(InputActionMap actionMap)
    {
        if (actionMap.enabled) return;
        
        inputActions.Disable();
        actionMapChange?.Invoke(actionMap);
        actionMap.Enable();
    }
}
