using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Compass : MonoBehaviour
{
    private Transform Target;
    private float Distance;
    private GameObject[] Cherries;
    private bool NoCherries = false;
    [SerializeField] private TextMeshProUGUI DistanceText;
    
    void Update()
    {
        NearestCherry();
        
        if (!NoCherries)
        {
            Distance = Vector3.Distance(transform.position, Target.position);
            DistanceText.text = "Distance To Objective: " + Math.Round(Distance);
            
            var lookPos = Target.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);
        }
        else
        {
            //start spinning
            transform.Rotate(0, 500 * Time.deltaTime, 0);
            DistanceText.text = "Distance To Objective: 0";
        }
        
    }

    void NearestCherry()
    {
        Cherries = GameObject.FindGameObjectsWithTag("Cherry");

        if (Cherries.Length == 0)
        {
            NoCherries = true;
        }
        else
        {
            foreach (var cherry in Cherries)
            {
                // check which cherry is closest
                if (Target == null)
                {
                    Target = cherry.transform;
                }
                else if (Vector3.Distance(transform.position, cherry.transform.position) <
                         Vector3.Distance(transform.position, Target.position))
                {
                    Target = cherry.transform;
                }
            }
        }
    }
}