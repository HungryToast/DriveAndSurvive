using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    [Header("Exit Conditions")] 
    [SerializeField] private float woodReq, waterReq, foodReq;

    [Header("Player Conditions")] 
    [SerializeField] private Inventory playerInventory;
    
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerExitScript>().AtExit(true);
            playerInventory = other.gameObject.GetComponent<Inventory>();
            if (playerInventory.GetWood() >= woodReq && playerInventory.GetWater() >= waterReq && playerInventory.GetFood() >= foodReq)
            {
                other.gameObject.GetComponent<PlayerExitScript>().CanExit(true);
                print("Requirements Met");
            }
            else
            {
                other.gameObject.GetComponent<PlayerExitScript>().CanExit(false);
                print("Requirements Not Met");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerExitScript>().CanExit(false);
                other.gameObject.GetComponent<PlayerExitScript>().AtExit(false);
            }
        }
    }
}

