using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectibles : MonoBehaviour
{
    public GameObject objectToSpawn; // The object to spawn
    public int numberToSpawn; // The number of objects to spawn
    public Terrain terrain; // The terrain to spawn the objects on
    public float SpawnRadius; // The radius to spawn the objects in
    public Collectible collectible;


    void Start()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            Vector3 SpawnPosition = new Vector3(Random.Range(
                    terrain.transform.position.x,
                    terrain.transform.position.x + terrain.terrainData.size.x), 0,
                Random.Range(terrain.transform.position.z,
                    terrain.transform.position.z + terrain.terrainData.size.z));

            SpawnPosition.y = terrain.SampleHeight(SpawnPosition);

            Collider[] obstacles = Physics.OverlapSphere(SpawnPosition, SpawnRadius, 1);

            if (obstacles.Length == 0)
            {
                Instantiate(objectToSpawn, SpawnPosition, Quaternion.identity);
            }
            else
            {
                i--;
            }

            collectible.LoadCollectibles();
        }
    }
}