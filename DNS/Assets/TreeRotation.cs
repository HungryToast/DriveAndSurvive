using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TreeRotation : MonoBehaviour
{
    
    void Start()
    {
        Quaternion r = transform.rotation;
        float randomScale = UnityEngine.Random.Range(3, 6);
        Vector3 scale = new Vector3(randomScale,randomScale,randomScale);
        transform.SetPositionAndRotation(transform.position, r * quaternion.Euler(0,UnityEngine.Random.Range(-360, 360),0));
        transform.localScale = scale;
    }

    
}
