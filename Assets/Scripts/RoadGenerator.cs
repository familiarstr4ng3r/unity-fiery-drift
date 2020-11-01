using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [Header("Generator")]
    [SerializeField] private float chunkSize = 10;
    [SerializeField] private int startCount = 5;
    [SerializeField] private GameObject[] prefabs = null;

    [Header("Other")]
    [SerializeField] private Transform car = null;
    [SerializeField] private float offset = 0;

    private int chunkIndex = 0;
    private BoxCollider boxCollider = null;
    private List<GameObject> chunks = new List<GameObject>();

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        GenerateChunks();
        UpdateCollider();
    }

    private void Update()
    {
        var chunk = chunks[0];

        if (car.position.z + offset > chunk.transform.position.z)
        {
            chunks.Remove(chunk);

            Destroy(chunk.gameObject);

            SpawnChunk(GetSpawnPosition(chunkIndex++));

            UpdateCollider();
        }
    }

    private void GenerateChunks()
    {
        for (int i = 0; i < startCount; i++)
        {
            SpawnChunk(GetSpawnPosition(chunkIndex++));
        }
    }

    private void SpawnChunk(Vector3 spawnPosition)
    {
        int randomIndex = Random.Range(0, prefabs.Length);
        var chunk = Instantiate(prefabs[randomIndex], spawnPosition, Quaternion.identity, transform);
        chunk.name = $"{chunkIndex}: Chunk";

        chunks.Add(chunk);
    }

    private Vector3 GetSpawnPosition(int index)
    {
        return Vector3.forward * chunkSize * index;// + Vector3.right * spaceAmountBetween * index;
    }

    private void UpdateCollider()
    {
        var size = Vector3.zero;
        size.x = 50;
        size.y = 1;
        size.z = chunkIndex * chunkSize;
        //size.z = chunks.Count * chunkSize;

        boxCollider.size = size;

        var center = Vector3.zero;
        center.z = chunkSize * 0.5f * (chunkIndex - 1);
        //center.z = chunkSize * 0.5f * (chunkIndex + 1);

        boxCollider.center = center;
    }
}
