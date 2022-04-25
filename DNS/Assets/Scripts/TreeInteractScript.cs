using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TreeInteractScript : MonoBehaviour
{
    [SerializeField]private Inventory playerInventory;
    [SerializeField]private Inputs playerInputs;
    [SerializeField]private Animator _animator;
    [SerializeField] private AnimationClip chopAnimation;
    [SerializeField]private GameObject player;
    [SerializeField] private int woodGain;
    [SerializeField] private InputAction chop;

    private bool inRange;
    
    private void Awake()
    {
        playerInputs = new Inputs();
        _animator = player.GetComponent<Animator>();
        
    }

    private void OnEnable()
    {
        playerInputs.Player.Interact.started += ChopWood;
    }

    private void ChopWood(InputAction.CallbackContext obj)
    {
       if(inRange) {
            _animator.SetTrigger("ChopTree");
            playerInventory.SetWood(woodGain);
            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
        }
    }

  

    
}
