using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    public GameObject wallPrefab;

    void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        float[,] heightmap = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapResolution,
            terrain.terrainData.heightmapResolution);

        Vector3 terrainSize = terrain.terrainData.size;
        Vector3 terrainPosition = terrain.transform.position;

        BoxCollider wallCollider = wallPrefab.GetComponent<BoxCollider>();
        float colliderSize = wallCollider.size.x;
        float offset = (colliderSize) / 20;

        for (int x = 0; x < terrain.terrainData.heightmapResolution; x++)
        {
            for (int y = 0; y < terrain.terrainData.heightmapResolution; y++)
            {
                // Check if this is an edge point
                if (x == 0 || x == terrain.terrainData.heightmapResolution - 1 || y == 0 ||
                    y == terrain.terrainData.heightmapResolution - 1)
                {
                    // Check if this is a corner point
                    if (!IsCorner(x, y, terrain.terrainData.heightmapResolution))
                    {
                        float height = heightmap[x, y];
                        Vector3 wallPosition = terrainPosition +
                                               new Vector3(
                                                   x * terrainSize.x / (terrain.terrainData.heightmapResolution - 1),
                                                   height * terrainSize.y,
                                                   y * terrainSize.z / (terrain.terrainData.heightmapResolution - 1));
                        Vector3 wallPositionWithOffset = wallPosition + (colliderSize + offset) * Vector3.forward;
                        GameObject wall = Instantiate(wallPrefab, wallPositionWithOffset, Quaternion.identity);
                        wall.transform.parent = terrain.transform;

                        // Rotate the wall to face the center of the terrain
                        Vector3 direction = (terrain.transform.position - wall.transform.position).normalized;
                        Vector3 normal = terrain.terrainData.GetInterpolatedNormal(
                            x / (float)terrain.terrainData.heightmapResolution,
                            y / (float)terrain.terrainData.heightmapResolution);

                        if (x == 0)
                        {
                            wall.transform.rotation = Quaternion.LookRotation(Vector3.left, normal);
                        }
                        else if (x == terrain.terrainData.heightmapResolution - 1)
                        {
                            wall.transform.rotation = Quaternion.LookRotation(Vector3.right, normal);
                        }
                        else if (y == 0)
                        {
                            wall.transform.rotation = Quaternion.LookRotation(Vector3.back, normal);
                        }
                        else if (y == terrain.terrainData.heightmapResolution - 1)
                        {
                            wall.transform.rotation = Quaternion.LookRotation(Vector3.forward, normal);
                        }
                    }
                }
            }
        }
    }

    private bool IsCorner(int x, int y, int heightmapResolution)
    {
        if ((x == 0 || x == heightmapResolution - 1) && (y == 0 || y == heightmapResolution - 1))
        {
            return true;
        }
        return false;
    }

}
