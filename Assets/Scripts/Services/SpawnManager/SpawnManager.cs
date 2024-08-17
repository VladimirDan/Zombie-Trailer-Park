using Assets.Scripts.Services.SpawnManager;
using Assets.Scripts.Services.SpawnManager.Factories;
using Game.Code.Common.CoroutineRunner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    CoroutineRunner coroutineRunner;
    DataProvider dataProvider;
    [SerializeField] public string spawnEntityType;
    [SerializeField] private Vector3 baseSpawnPosition;
    [SerializeField] private float spawnCooldown;
    private Quaternion baseRotation = Quaternion.Euler(0, 0, 0);
    [SerializeField] public int maxSpawnCount = 10;
    [SerializeField] private float maxDistanceBetweenRows = 5;

    void Start()
    {
        coroutineRunner = FindObjectOfType<CoroutineRunner>();
        dataProvider = GetComponent<DataProvider>();
        coroutineRunner.RunCoroutine(startSpawnCycle(baseSpawnPosition));
    }

    public IEnumerator startSpawnCycle(Vector3 baseSpawnPosition)
    {
        for (int i = 0; i < maxSpawnCount; i++)
        {
            Vector3 spawnPosition = GenerateEntitySpawnPosition(baseSpawnPosition);
            SpawnObject(spawnEntityType, spawnPosition);
            yield return new WaitForSeconds(spawnCooldown);
        }
    }

    public void SpawnObject(string entityType, Vector3 position)
    {
        EntitySpawner spawner = entityType switch
        {
            "Zombie" or "Digger" => spawner = new StandartUnitSpawner(dataProvider),
            _ => null
        };

        spawner.SpawnEntity(entityType, position);
    }

    public Vector3 GenerateEntitySpawnPosition(Vector3 baseSpawnPosition)
    {
        float zCoordinate = baseSpawnPosition.z + PickRandomLineForUnitWalkWay(maxDistanceBetweenRows / 2, -maxDistanceBetweenRows / 2);
        return new Vector3(baseSpawnPosition.x, baseSpawnPosition.y, zCoordinate);
    }

    public float PickRandomLineForUnitWalkWay(float minValue, float maxValue)
    {
        float step = (maxValue - minValue) / 4.0f;
        float[] possibleValues = new float[5];
        for (int i = 0; i < possibleValues.Length; i++)
        {
            possibleValues[i] = minValue + i * step;
        }
        int randomIndex = Random.Range(0, possibleValues.Length);
        return possibleValues[randomIndex];
    }
}
