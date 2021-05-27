using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoadSpawner : MonoBehaviour
{
    public GameObject[] RoadBlockPrefabs;
    public GameObject[] CityBlockPrefabs;
    public GameObject StartRoadBlock;
    public GameObject StartCityBlock;

    float blockRoadXPos = 0; 
    float blockCityXPos = 0;
    int blocksRoadCount = 20; 
    int blockCityCount = 104;
    float blockRoadLength = 0;
    float blockCityLength = 4;
    int safeZone = 60;

    public Transform PlayerTransf;
    List<GameObject> CurrentBlocksRoad = new List<GameObject>();
    List<GameObject> CurrentBlocksCityRight = new List<GameObject>();
    List<GameObject> CurrentBlocksCityLeft = new List<GameObject>();

    void Start()
    {
        blockRoadXPos = StartRoadBlock.transform.position.x;
        blockCityXPos = StartCityBlock.transform.position.x;
        blockRoadLength = StartRoadBlock.transform.localScale.x;

        for (int i = 0; i < blocksRoadCount; i++)
            SpawnRoadBlock();
        for (int i = 0; i < blockCityCount; i++)
            SpawnCityBlock();
    }

    void Update()
    {
        CheckForSpawn();
    }

    void CheckForSpawn()
    {
        if (PlayerTransf.position.x - safeZone > blockRoadXPos - blocksRoadCount * blockRoadLength)
        {
            SpawnRoadBlock();
            for (int i = 0; i < 5; i++)
                SpawnCityBlock();
            DestroyBlockRoad();
            DestroyBlockCity(PlayerTransf.position.x);
        }
    }

    void SpawnRoadBlock()
    {
        GameObject blockRoad = Instantiate(RoadBlockPrefabs[Random.Range(0, RoadBlockPrefabs.Length)], transform);
        blockRoadXPos += blockRoadLength;
        blockRoad.transform.position = new Vector3(blockRoadXPos, 0, 0);
        CurrentBlocksRoad.Add(blockRoad);
    }

    void SpawnCityBlock()
    {
        GameObject blockCityRight = Instantiate(CityBlockPrefabs[Random.Range(0, CityBlockPrefabs.Length)], transform);
        GameObject blockCityLeft = Instantiate(CityBlockPrefabs[Random.Range(0, CityBlockPrefabs.Length)], transform);
        blockCityXPos += blockCityLength;
        blockCityRight.transform.position = new Vector3(blockCityXPos, 0, -0.28f);
        blockCityLeft.transform.position = new Vector3(blockCityXPos, 0, 8.7f);
        CurrentBlocksCityRight.Add(blockCityRight);
        CurrentBlocksCityLeft.Add(blockCityLeft);
    }
    
    void DestroyBlockRoad()
    {
        Destroy(CurrentBlocksRoad[0]);
        CurrentBlocksRoad.RemoveAt(0);
    }

    void DestroyBlockCity(float x)
    {
        int blocks = 0;
        if (x < 65)
            blocks = 4;
        else
            blocks = 5;
        for (int i = 0; i < blocks; i++)
        {
            Destroy(CurrentBlocksCityRight[i]);
            Destroy(CurrentBlocksCityLeft[i]);
            CurrentBlocksCityRight.RemoveAt(i);
            CurrentBlocksCityLeft.RemoveAt(i);
        }

    }
}
