using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class MonsterDifficulty : MonoBehaviour
{
    [SerializeField] Collectible collectible;
    [SerializeField] float ItemsCollected;
    [SerializeField] Terrain terrain;
	

    public float ObstacleRadius;
    private bool Spawned = false;
    public GameObject monster;
    public float MonsterSpeed;

    // Update is called once per frame
    void Update()
    {
        ItemsCollected = collectible.ItemsCollected;
		MonsterSpeed = ItemsCollected * 1.7f;
        SpawnMonster();
    }

    void SpawnMonster()
    {
        if (ItemsCollected >= 1 && !Spawned)
        {
            Vector3 playerPosition = GameObject.Find("Player").transform.position;
            float angle = Random.Range(-180f, 180f);
            Vector3 spawnDirection = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));            
            Vector3 SpawnPosition = playerPosition + (spawnDirection * 20);
            SpawnPosition.y = terrain.SampleHeight(SpawnPosition);
        
            Collider[] obstacles = Physics.OverlapSphere(SpawnPosition, ObstacleRadius, 1);
        
            if (obstacles.Length == 0)
            {
                monster.transform.position = SpawnPosition;
                monster.SetActive(true);
            }
            Spawned = true;
        }
    }
}
