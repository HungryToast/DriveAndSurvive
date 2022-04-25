using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject castPoint;
    

    void Attack()
    {
        RaycastHit hit;
        Ray ray = new Ray(castPoint.transform.position, Vector3.forward);

        if (Physics.SphereCast(ray, 0.3f, out hit,0.3f))
        {
            if (hit.collider.tag == "Destroyable")
            {
                
            }
        }

        

    }
}
