using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    public GameObject[] tiles;
    public Transform playerTransform;

    public GameObject[] powerUpPrefabs;
    public float powerUpSpawnChance = 0.2f;

    public float zSpawnPos = 0;
    private float tileLength = 30;
    public int numberOfTiles = 5;
    private List<GameObject> activeTiles = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
            {
                SpawnTile(0);
            } else
            {
                SpawnTile(Random.Range(0, tiles.Length));
            }
        }
    }

    public void Update()
    {
        if(playerTransform.position.z - 30 > zSpawnPos - (numberOfTiles * tileLength))
        {
            SpawnTile(Random.Range(0, tiles.Length));
            DestroyTile();
        }
    }

    public void SpawnTile(int tileIndex)
    {
        GameObject tile = Instantiate(tiles[tileIndex], new Vector3(0, 0, zSpawnPos), transform.rotation);

        // Try to find a child named "PowerUpPoint" under the tile
        Transform powerUpPoint = tile.transform.Find("PowerUpPoint");

        // Randomly decide whether to spawn a power-up
        if (powerUpPoint != null && Random.value <= powerUpSpawnChance)
        {
            int randIndex = Random.Range(0, powerUpPrefabs.Length);
            Instantiate(powerUpPrefabs[randIndex], powerUpPoint.position, Quaternion.identity);
        }

        zSpawnPos += tileLength;
        activeTiles.Add(tile);
    }

    private void DestroyTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
