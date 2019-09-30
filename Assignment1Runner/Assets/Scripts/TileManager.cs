using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;

    private Transform playerTransform;
    private float spawnZ = -3.04f;
    private float tileLength = 8.04f;
    private float safeZone = 20.0f;
    private int amtTilesOnScreen = 10;
    private int lastPrefab = 0;
    private List<GameObject> activeTiles;
    // Start is called before the first frame update
    void Start()
    {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i=0; i<amtTilesOnScreen;i++)
        {
            if (i < 1)
            {
               //SpawnTile(0);
                
            }
            else
            {
                SpawnTile();
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //
        if (playerTransform.position.z - safeZone > (spawnZ-amtTilesOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }

    private void SpawnTile(int prefabIndex=-1)
    {
        GameObject go;
        if (prefabIndex==-1) {
            go = Instantiate(tilePrefabs[RandomPrefabIndex()] as GameObject);
            go.transform.SetParent(transform);
            go.transform.position = Vector3.forward * spawnZ;
            spawnZ += tileLength;
            activeTiles.Add(go);
        }
        else
        {
            go = Instantiate(tilePrefabs[prefabIndex]);
        }
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        if (tilePrefabs.Length<=1)
        {
            return 0;
        }
        int randomIndex = lastPrefab;
        while (randomIndex == lastPrefab)
        {
            randomIndex = Random.Range(0,tilePrefabs.Length);
        }
        lastPrefab = randomIndex;
        return randomIndex;
    }

}
