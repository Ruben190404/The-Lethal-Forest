using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDifficulty : MonoBehaviour
{
    [SerializeField] Collectible collectible;
    [SerializeField] float ItemsCollected;
    [SerializeField] GameObject monster;
    [SerializeField] Terrain terrain;
    public float ObstacleRadius;
    private int difficulty = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        difficulty = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ItemsCollected = collectible.ItemsCollected;
    }

    void SetDifficulty()
    {
        if (ItemsCollected == 1)
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
        }
    }
}
