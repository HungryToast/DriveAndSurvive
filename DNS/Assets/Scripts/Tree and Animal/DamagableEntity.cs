using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using Random = Unity.Mathematics.Random;


public class DamagableEntity : MonoBehaviour
{
    // This is a class for any entity that can be damaged
    
    [Header("Entity Stats")]
    [SerializeField] int healthPoints;
    [SerializeField] private float dropGain;

    [Header("Inventory Reference")]
    [SerializeField] private Inventory inventory;

    private void Start()
    {
        if (gameObject.CompareTag("Tree"));
        {
            healthPoints = 20;
        }

        if (gameObject.CompareTag("Animal"))
        {
            healthPoints = 20;
        }

        inventory = FindObjectOfType<Inventory>();
    }

    private void OnEnable()
    {
        if(CompareTag("Tree"))
        {
            dropGain = UnityEngine.Random.Range(1, 5);
        }

        if (CompareTag("Animal"))
        {
            dropGain = UnityEngine.Random.Range(1, 10);
        }
    }

    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
        if(CompareTag("Animal")) GetComponent<EnemyAI>().BecomeHostile();
    }

    private void Update()
    {
        // Kill
        if (healthPoints <= 0)
        {
            Destroy(gameObject);
            Drop();
        }
    }

    private void Drop()
    {
        if (this.CompareTag("Tree"))
        {
            inventory.SetWood((int) dropGain);
        }

        if (this.CompareTag("Animal"))
        {
            inventory.SetFood((int) dropGain);
        }
    }

    

}
