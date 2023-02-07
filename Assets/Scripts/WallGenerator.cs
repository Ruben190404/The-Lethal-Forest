using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    public GameObject wallPrefab;

    void Start()
    {
        // Get the terrain component attached to the same GameObject
        Terrain terrain = GetComponent<Terrain>();
        // Get the heightmap data of the terrain
        float[,] heightmap = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapResolution,
            terrain.terrainData.heightmapResolution);

        // Get the size and position of the terrain
        Vector3 terrainSize = terrain.terrainData.size;
        Vector3 terrainPosition = terrain.transform.position;

        // Loop through each point in the heightmap
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
                        // Get the height of the point
                        float height = heightmap[x, y];
                        // Calculate the position of the wall to be instantiated
                        Vector3 wallPosition = terrainPosition +
                                               new Vector3(
                                                   x * terrainSize.x / (terrain.terrainData.heightmapResolution - 1),
                                                   height * terrainSize.y,
                                                   y * terrainSize.z / (terrain.terrainData.heightmapResolution - 1));
                        // Instantiate the wallPrefab at the calculated position
                        GameObject wall = Instantiate(wallPrefab, wallPosition, Quaternion.identity);
                        // Set the parent of the wall to be the terrain
                        wall.transform.parent = terrain.transform;

                        // Rotate the wall to face the center of the terrain
                        Vector3 direction = (terrain.transform.position - wall.transform.position).normalized;
                        Vector3 normal = terrain.terrainData.GetInterpolatedNormal(
                            x / (float)terrain.terrainData.heightmapResolution,
                            y / (float)terrain.terrainData.heightmapResolution);

                        // If the x value is equal to 0, it means the wall is at the left edge of the terrain
                        // Rotate the wall to face the left direction, with the normal of the terrain surface
                        if (x == 0)
                        {
                            wall.transform.rotation = Quaternion.LookRotation(Vector3.left, normal);
                        }
                        // If the x value is equal to the heightmap resolution minus 1, it means the wall is at the right edge of the terrain
                        // Rotate the wall to face the right direction, with the normal of the terrain surface
                        else if (y == terrain.terrainData.heightmapResolution - 1)
                        {
                            wall.transform.rotation = Quaternion.LookRotation(Vector3.forward, normal);
                            // If y == terrain.terrainData.heightmapResolution - 1, the wall is rotated to face forward, facing the center of the terrain.
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
            // The method checks if the point (x, y) is a corner point of the terrain by checking if either x or y is 0 or equal to heightmapResolution - 1.
            return true;
        }

        return false;
    }
}
