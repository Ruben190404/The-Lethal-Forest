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
	[SerializeField] float DefaultSpeed;

    public float ObstacleRadius;
    private bool Spawned = false;
    public GameObject monster;
    public float MonsterSpeed;

    // Update is called once per frame
    void Update()
    {
        ItemsCollected = collectible.ItemsCollected;
		MonsterSpeed = (ItemsCollected * DefaultSpeed) * 1.7f;
        SpawnMonster();
    }

    void SpawnMonster()
    {
        if (ItemsCollected >= 1 && !Spawned)
        {
            Vector3 SpawnPosition = new Vector3(Random.Range(
                    terrain.transform.position.x,
                    terrain.transform.position.x + terrain.terrainData.size.x), 0,
                Random.Range(terrain.transform.position.z,
                    terrain.transform.position.z + terrain.terrainData.size.z));
        
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
