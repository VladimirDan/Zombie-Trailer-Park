using Game.Code.Common.CoroutineRunner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    CoroutineRunner coroutineRunner;
    [SerializeField] private GameObject entityPrefab;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private float spawnCooldown;
    private Quaternion baseRotation = Quaternion.Euler(0, 0, 0);
    [SerializeField] public int maxSpawnCount = 10;

    void Start()
    {
        coroutineRunner = FindObjectOfType<CoroutineRunner>();
        coroutineRunner.RunCoroutine(startSpawnCycle(spawnPosition, baseRotation, entityPrefab));
    }

    public IEnumerator startSpawnCycle(Vector3 position, Quaternion rotation, GameObject objectPrefab)
    {
        for (int i = 0; i < maxSpawnCount; i++)
        {
            SpawnObject(position, rotation, objectPrefab);
            yield return new WaitForSeconds(spawnCooldown);
        }
    }

    public void SpawnObject(Vector3 position, Quaternion rotation, GameObject objectPrefab)
    {
        Instantiate(objectPrefab, new Vector3(position.x, position.y, position.z), rotation);
    }
}
