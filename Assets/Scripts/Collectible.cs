using System;
using TMPro;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    MonsterDifficulty monsterDifficulty;
    public int TotalItems;
    public float ItemsCollected = 0;

    private Transform Target;
    private float Distance;
    private GameObject[] Cherries;
    private bool NoCherries = false;
    bool MonsterSpawned = false;

    [SerializeField] private GameObject Monster;
    [SerializeField] private TextMeshProUGUI DistanceText;
    [SerializeField] private TextMeshProUGUI Text;
    [SerializeField] private AudioSource collectionSoundEffect;

    private void Start()
    {
        monsterDifficulty = GameObject.Find("Monster").GetComponent<MonsterDifficulty>();
        MonsterSpawned = monsterDifficulty.Spawned;
    }

    private void Update()
    {
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
        //teleport monster 10 units away from player in any direction
        Monster.transform.position = transform.position +
                                     new Vector3(UnityEngine.Random.Range(-10, 10), 0,
                                         UnityEngine.Random.Range(-10, 10));
    }
}