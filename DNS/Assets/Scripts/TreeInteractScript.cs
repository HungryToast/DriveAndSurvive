using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TreeInteractScript : MonoBehaviour
{
    [SerializeField] int hP; 
    [SerializeField] int woodGain;

    [SerializeField] GameObject player;
    
    private void Awake()
    {
        hP = 5;
        player = GameObject.FindWithTag("Player");
    }

    public int ReduceHP()
    {
        return hP--;
    }
    
    
}
