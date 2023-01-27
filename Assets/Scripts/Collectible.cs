using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{
    int TotalItems;

    public float ItemsCollected = 0;
    [SerializeField] private TextMeshProUGUI Text;
    [SerializeField] private AudioSource collectionSoundEffect;
    
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            Destroy(collision.gameObject);
            ItemsCollected++;
            Text.text = ItemsCollected + "/" + TotalItems;
            collectionSoundEffect.Play();
        }
    }

    public void LoadCollectibles()
    {
        TotalItems = GameObject.FindGameObjectsWithTag("Cherry").Length;
        Text.text = ItemsCollected + "/" + TotalItems;
    }
}
