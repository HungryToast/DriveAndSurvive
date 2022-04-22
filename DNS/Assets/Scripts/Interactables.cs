using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactables : MonoBehaviour
{
    public UnityEvent unityEvent;

    public void InvokeInteraction() { 
        unityEvent.Invoke();
    }
}
