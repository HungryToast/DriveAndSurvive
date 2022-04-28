using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Active Player Stats")]
    [SerializeField] private float stamina, thirst, hunger, health;
    
    [Header("Default Player Stats")]
    [SerializeField] private float defaultHealth;
    [SerializeField] private float defaultStamina, defaultThirst, defaultHunger;

    [Header("Default Drain Stats")]
    [SerializeField] private float defaultHungerDrain, defaultHealthDrain;
    
    [Header("Player Components")] 
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator playerAnimator;

    private bool drainHunger;


//Awake
    private void Awake()
    {
        playerController = this.gameObject.GetComponent<PlayerController>();
        playerAnimator = this.gameObject.GetComponent<Animator>();
    }

    // Set Stats
    private void Start()
    {
        health = defaultHealth;
        stamina = defaultStamina;
        thirst = defaultThirst;
        hunger = defaultHunger;

        drainHunger = true;
    }


// Gain Stats
    public float GainHealth(float gainValue)
    {
        if (health < defaultHealth)
        {
            return health += gainValue;
        }
        else
        {
            return health;
        }
    }

    public float GainStamina(float gainValue)
    {
        if (stamina < defaultStamina)
        {
            return stamina += gainValue;
        }
        else
        {
            return stamina;
        }
    }

    public float GainThirst(float gainValue)
    {
        if (thirst < defaultThirst)
        {
            return thirst += gainValue;
        }
        else
        {
            return thirst;
        }
    }

    public float GainHunger(float gainValue)
    {
        if (hunger < defaultHunger)
        {
            return hunger += gainValue;
        }
        else
        {
            return hunger;
        }
    }
    
// Drain Stats
    public float DrainHealth(float drainValue)
    {
        return health -= drainValue;
    }
    public float DrainStamina(float drainValue)
    {
       return stamina -= drainValue;
    }

    public float DrainThirst(float drainValue)
    {
        return thirst -= drainValue;
    }

    public float DrainHunger(float drainValue)
    {
        return hunger -= drainValue;
    }

    private void Update()
    {
        //Kill player when health is <= than 0
        if (health <= 0)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        if(playerAnimator.GetBool("isRunning") && drainHunger)
        {
            DrainHunger(defaultHungerDrain * 2f);
        }
        else if(drainHunger)
        {
            DrainHunger(defaultHungerDrain);
        }
        

        if (hunger > 0)
        {
            drainHunger = true;
        }
        else if (hunger <= 0)
        {
            drainHunger = false;
            hunger = 0;
        }


        if (!playerAnimator.GetBool("isRunning") && !playerAnimator.GetBool("isWalking") && hunger>0)
        {
            GainStamina(1f);
            DrainHunger(0.08f);
        }
        else if (stamina > 0 && playerAnimator.GetBool("isRunning"))
        {
            DrainStamina(0.2f);
        }
        
        if (stamina <= 0)
        {
            playerController.SetCanUseStamina(false);
        }
        else
        {
            playerController.SetCanUseStamina(true);   
        }

        if (stamina > defaultStamina)
        {
            stamina = defaultStamina;
        }

        if (hunger <= 0)
        {
            DrainHealth(defaultHealthDrain);
        }
    }

    private void Die()
    {
        playerAnimator.SetTrigger("Die");
        Destroy(this.gameObject);
    }
}
