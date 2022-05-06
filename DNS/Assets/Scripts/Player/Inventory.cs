using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private Inputs inputAsset;

    [SerializeField] GameObject axe, foodObject, waterObject;
    [SerializeField] private TMP_Text waterUI, foodUI, woodUI;
    
    private PlayerController _playerController;
    private PlayerStats _playerStats;
    
    [SerializeField] int wood;
    [SerializeField] int food;
    [SerializeField] float water;

    
    private void FixedUpdate()
    {
        woodUI.text = "Wood X" + wood;
        waterUI.text = "Water X" + water;
        foodUI.text = "Food X" + food;
    }

    private void OnEnable()
    {
        inputAsset.Player.Weapon.started += SelectWeapon;
        inputAsset.Player.Food.started += SelectFood;
        inputAsset.Player.Water.started += SelectWater;
        inputAsset.Player.Use.started += UseItem;
    }

    private void UseItem(InputAction.CallbackContext obj)
    {
        if (waterObject.activeSelf)
        {
            if (water > 0)
            {
                gameObject.GetComponent<Animator>().SetTrigger("UseItem");
                _playerStats.GainThirst(10);
                water -= 10;
            }
        }
        
        
        else if(foodObject.activeSelf)
        {
            if (food > 0)
            {
                gameObject.GetComponent<Animator>().SetTrigger("UseItem");
                _playerStats.GainHunger(10);
                food--;
            }
        }
    }

    private void SelectWeapon(InputAction.CallbackContext obj)
    {
        _playerController.CanAttackMethod(true);
        foodObject.SetActive(false);
        waterObject.SetActive(false);
        axe.SetActive(true);
    }
    private void SelectFood(InputAction.CallbackContext obj)
    {
        _playerController.CanAttackMethod(false);
        foodObject.SetActive(true);
        waterObject.SetActive(false);
        axe.SetActive(false);
    }
    private void SelectWater(InputAction.CallbackContext obj)
    {
        _playerController.CanAttackMethod(false);
        foodObject.SetActive(false);
        waterObject.SetActive(true);
        axe.SetActive(false);
    }

    private void Awake()
    {
        _playerController = gameObject.GetComponent<PlayerController>();
        inputAsset = new Inputs();
        _playerStats = gameObject.GetComponent<PlayerStats>();
        foodObject.SetActive(false);
        waterObject.SetActive(false);
    }
    
    
    
    
    private void Start()
    {
        inputAsset.Player.Enable();
    }

    public int GetWood()
    {
        return wood;
    }

    public int SetWood(int gain)
    {
        wood += gain;
        return wood;
    }

    public int GetFood()
    {
        return food;
    }

    public int SetFood(int gain)
    {
        return food += gain;
    }

    public float GetWater()
    {
        return water;
    }

    public float SetWater(float gain)
    {
        return water += gain;
    }

}
