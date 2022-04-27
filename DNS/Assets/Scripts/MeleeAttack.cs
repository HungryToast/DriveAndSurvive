using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Tree"))
        {
            var tree = other.gameObject.GetComponent<DamagableEntity>();
            tree.TakeDamage(1);
        }

        else if (other.CompareTag("Animal"))
        {
            var animal = other.gameObject.GetComponent<DamagableEntity>();
            animal.TakeDamage(UnityEngine.Random.Range(1,20));
        }
        
    }

    
}
