using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{

    private float Score;
    
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private int CherryValue;

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            Score += CherryValue;
            ScoreText.text = "Score: " + Score;
            Destroy(collision.gameObject);
        }
    }
}
