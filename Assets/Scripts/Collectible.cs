using System;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Collectible : MonoBehaviour
{
    [SerializeField] private MonsterDifficulty monsterDifficulty;
    public int TotalItems;
    public float ItemsCollected = 0;
    public bool Teleported = false;

    private Transform Target;
    private float Distance;
    private GameObject[] Cherries;
    private bool NoCherries = false;
    public bool MonsterSpawned = false;


    [SerializeField] private Transform Player;
    [SerializeField] private GameObject Monster;
    [SerializeField] private TextMeshProUGUI DistanceText;
    [SerializeField] private TextMeshProUGUI Text;
    [SerializeField] private AudioSource collectionSoundEffect;

    private void Update()
    {
        MonsterSpawned = monsterDifficulty.Spawned;
        if (MonsterSpawned)
        {
            Monster = GameObject.FindGameObjectWithTag("Monster");
        }

        NearestCherry();
        if (!NoCherries)
        {
            Distance = Vector3.Distance(transform.position, Target.position);
            DistanceText.text = "Distance To Objective: " + Math.Round(Distance);
        }
        else
        {
            DistanceText.text = "Distance To Objective: 0";
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            Destroy(collision.gameObject);
            ItemsCollected++;
            Text.text = "Collected: " + ItemsCollected + "/" + TotalItems;
            collectionSoundEffect.Play();
            TeleportMonster();
        }
    }

    public void LoadCollectibles()
    {
        TotalItems = GameObject.FindGameObjectsWithTag("Cherry").Length;
        Text.text = ItemsCollected + "/" + TotalItems;
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

    void TeleportMonster()
    {
        if (MonsterSpawned)
        {
            Monster.GetComponent<NavMeshAgent>()
                .Warp(Player.position + new Vector3(Random.Range(5, 15), 0, Random.Range(5, 15)));
            Monster.transform.LookAt(Player);
            Teleported = true;
        }
    }
}