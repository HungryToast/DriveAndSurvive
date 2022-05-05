using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
   [SerializeField] private GameObject animal;
   [SerializeField] private int maxAnimals;
   private Vector3 position;


   private void Start()
   {
      position = transform.position;
      Spawn();
   }

   void Spawn()
   {
      for (int i = 0; i < maxAnimals; i++)
      {
         Instantiate(animal, position, Quaternion.identity);
      }
   }
}
