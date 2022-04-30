using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSourceScript : MonoBehaviour
{
   


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) other.gameObject.GetComponent<PlayerController>().AtWaterSource(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))other.gameObject.GetComponent<PlayerController>().AtWaterSource(false);
    }
    
    
}
