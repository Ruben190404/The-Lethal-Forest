using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    public Transform target;
    void Update()
    {
        var lookPos = target.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);
    }
    
}
