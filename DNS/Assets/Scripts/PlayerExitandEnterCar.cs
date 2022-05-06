using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExitandEnterCar : MonoBehaviour
{
    [SerializeField] private GameObject car, player, playerInCar;
    

    public void ToggleEnterExit()
    {
        player.SetActive(!player.activeSelf);
        playerInCar.SetActive(!player.activeSelf);
    }

    public void SpawnPlayerNextToCar()
    {
        player.SetActive(true);

        player.transform.position = car.transform.position - transform.TransformDirection(Vector3.left);
    }
    
}
