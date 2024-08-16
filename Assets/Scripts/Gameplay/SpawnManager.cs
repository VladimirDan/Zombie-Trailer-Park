using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject entityPrefab;
    [SerializeField] private Vector2 spawnPosition;
    [SerializeField] private float spawnCooldown;
    private Quaternion baseRotation = Quaternion.Euler(0, 0, 0);
    [SerializeField] public int maxSpawnCount = 10;

    void Start()
    {
        StartCoroutine(startSpawnCycle(spawnPosition, baseRotation, entityPrefab));
    }

    public IEnumerator startSpawnCycle(Vector2 position, Quaternion rotation, GameObject objectPrefab)
    {
        for (int i = 0; i < maxSpawnCount; i++)
        {
            SpawnObject(position, rotation, objectPrefab);
            yield return new WaitForSeconds(spawnCooldown);
        }
    }

    public void SpawnObject(Vector2 position, Quaternion rotation, GameObject objectPrefab)
    {
        Instantiate(objectPrefab, new Vector3(position.x, position.y, 0), rotation);
    }
}
