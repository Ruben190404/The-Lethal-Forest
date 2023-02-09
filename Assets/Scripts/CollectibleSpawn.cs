using UnityEngine;

public class CollectibleSpawn : MonoBehaviour
{
    public GameObject objectToSpawn;
    public int numberToSpawn;
    public Terrain terrain;
    public float ObstacleRadius;
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

            Collider[] obstacles = Physics.OverlapSphere(SpawnPosition, ObstacleRadius, 3);

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