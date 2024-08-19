using Assets.Scripts.Services.SpawnManager;
using Assets.Scripts.Services.SpawnManager.Factories;
using CreaturesData;
using Game.Code.Common.CoroutineRunner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    CoroutineRunner coroutineRunner;
    DataProvider dataProvider;
    [SerializeField] public UnitType basicEntityType;
    [SerializeField] public ZombieSpawnTimings spawnTimings;
    [SerializeField] private Vector3 baseSpawnPosition;
    [SerializeField] private float zombieSpawnCooldown;
    [SerializeField] private float zombieJuperSpawnCooldown;
    [SerializeField] private float bansheeSpawnCooldown;
    [SerializeField] private float giantSpawnCooldown;
    private Quaternion baseRotation = Quaternion.Euler(0, 0, 0);
    [SerializeField] public int maxSpawnCount = 10;
    [SerializeField] private float maxDistanceBetweenRows = 5;

    public void Initialize()
    {
        coroutineRunner = FindObjectOfType<CoroutineRunner>();
        dataProvider = GetComponent<DataProvider>();

        coroutineRunner.RunCoroutine(OrderZombieSpawnCooldownChange());
        OrderSpecialZombiesSpawn();

        coroutineRunner.RunCoroutine(startSpawnCycle(baseSpawnPosition));
    }

    public IEnumerator OrderZombieSpawnCooldownChange()
    {
        foreach(var data in spawnTimings.zombieSpawmCooldownChange)
        {
            zombieSpawnCooldown = data.spawnCooldown;
            yield return new WaitForSeconds(data.timing);
        }
        yield break;
    }

    public void OrderSpecialZombiesSpawn()
    {
        foreach (var data in spawnTimings.specialZombieSpawnTimings)
        {
            foreach (float spawnTiming in data.spawnTimings)
            {
                coroutineRunner.RunCoroutine(OrderZombieSpawn(data.specialZombieType, spawnTiming));
            }
        }
    }

    public IEnumerator OrderZombieSpawn(UnitType zombieType, float timing)
    {
        yield return new WaitForSeconds(timing);
        SpawnUnitOnRandomRow(zombieType, baseSpawnPosition);
    }

    public IEnumerator startSpawnCycle(Vector3 baseSpawnPosition)
    {
        for (int i = 0; i < maxSpawnCount; i++)
        {
            yield return new WaitForSeconds(zombieSpawnCooldown);
            SpawnUnitOnRandomRow(basicEntityType, baseSpawnPosition);
        }
    }

    public void SpawnUnitOnRandomRow(UnitType entityType, Vector3 baseSpawnPosition)
    {
        Vector3 spawnPosition = GenerateEntitySpawnPosition(baseSpawnPosition);
        SpawnEntity(entityType, spawnPosition);
    }

    public void SpawnEntity(UnitType entityType, Vector3 position)
    {
        EntitySpawner spawner = entityType switch
        {
            UnitType.Zombie or UnitType.Banshee or UnitType.Digger
            or UnitType.Cleric or UnitType.SurvivalistCar or UnitType.Shooter => spawner = new StandartUnitSpawner(dataProvider),
            UnitType.Giant or UnitType.Boozer => spawner = new SplashDamageUnitSpawner(dataProvider),
            UnitType.ZombieJumper => spawner = new ZombieJumperSpawner(dataProvider),
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
