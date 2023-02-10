using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterDifficulty : MonoBehaviour
{
    [SerializeField] Collectible collectible;
    [SerializeField] float ItemsCollected;
    [SerializeField] Terrain terrain;
    [SerializeField] Transform Player;

    public float ObstacleRadius;
    public bool Spawned = false;
    public GameObject monster;
    public float MonsterSpeed;
    public bool Scream;

    void Update()
    {
        ItemsCollected = collectible.ItemsCollected;
        MonsterSpeed = ItemsCollected * 2;
		
        SpawnMonster();
    }

    void SpawnMonster()
    {
        if (ItemsCollected >= 1 && !Spawned)
        {
            Vector3 playerPosition = GameObject.Find("Player").transform.position;
            float angle = Random.Range(-180f, 180f);
            Vector3 spawnDirection = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));            
            Vector3 SpawnPosition = playerPosition + (spawnDirection * 5);
            SpawnPosition.y = terrain.SampleHeight(SpawnPosition);
        
            Collider[] obstacles = Physics.OverlapSphere(SpawnPosition, ObstacleRadius, 1);
        
            if (obstacles.Length == 0)
            {
                monster.transform.position = SpawnPosition;
                monster.transform.LookAt(Player);
                Scream = true;
                monster.SetActive(true);
            }
            Spawned = true;
        }
    }
}
