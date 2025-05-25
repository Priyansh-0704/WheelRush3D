using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    public GameObject[] tiles;
    public Transform playerTransform;

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
        zSpawnPos += tileLength;

        activeTiles.Add(tile);
    }

    private void DestroyTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
