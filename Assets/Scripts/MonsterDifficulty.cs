using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDifficulty : MonoBehaviour
{
    [SerializeField] Collectible collectible;
    [SerializeField] float ItemsCollected;
    [SerializeField] GameObject monster;
    [SerializeField] Terrain terrain;
	[SerializeField] float DefaultSpeed;
	public float MonsterSpeed;

    public float ObstacleRadius;
    private bool Spawned = false;
    
    // Update is called once per frame
    void Update()
    {
        ItemsCollected = collectible.ItemsCollected;
		MonsterSpeed = ItemsCollected * DefaultSpeed;
        SpawnMonster();
    }

    void SpawnMonster()
    {
        if (ItemsCollected == 1 && !Spawned)
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
                Instantiate(monster, SpawnPosition, Quaternion.identity);
            }
            Spawned = true;
        }
    }
}
