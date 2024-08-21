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
    private Vector3 baseSpawnPosition;
    private Quaternion baseRotation = Quaternion.Euler(0, 0, 0);
    private float maxDistanceBetweenRows = 5;

    [SerializeField] public ZombiesSpawnTimings zombiesSpawnCycleParameters;

    public void Initialize()
    {
        coroutineRunner = FindObjectOfType<CoroutineRunner>();
        dataProvider = GetComponent<DataProvider>();

        OrderZombiesSpawn();
    }

    public void OrderZombiesSpawn()
    {
        foreach (var unit in zombiesSpawnCycleParameters.zombiesSpawnTimings)
        {
            foreach (ZombieSpawnCycleParameters spawnParameters in unit.zombieSpawnCycleParameters)
            {
                coroutineRunner.RunCoroutine(startSpawnCycle(baseSpawnPosition, spawnParameters.timing, unit.zombieType, 
                    spawnParameters.spawnCooldown, spawnParameters.zombieCount));
            }
        }
    }

    public IEnumerator OrderZombieSpawn(UnitType zombieType, float timing)
    {
        yield return new WaitForSeconds(timing);
        SpawnUnitOnRandomRow(zombieType, baseSpawnPosition);
    }

    public IEnumerator startSpawnCycle(Vector3 baseSpawnPosition, float timing, UnitType zombieType, float zombieSpawnCooldown, int zombieCount)
    {
        yield return new WaitForSeconds(timing);

        for (int i = 0; i < zombieCount; i++)
        {
            yield return new WaitForSeconds(zombieSpawnCooldown);
            SpawnUnitOnRandomRow(zombieType, baseSpawnPosition);
        }
    }

    public void SpawnUnitOnRandomRow(UnitType entityType, Vector3 baseSpawnPosition)
    {
        Vector3 spawnPosition = GenerateEntitySpawnPosition(baseSpawnPosition);
        SpawnEntity(entityType, spawnPosition);
    }

    public void SpawnEntity(UnitType entityType, Vector3 position)
    {
        UnitSpawner spawner = entityType switch
        {
            UnitType.Zombie or UnitType.Banshee or UnitType.Digger
            or UnitType.Cleric or UnitType.SurvivalistCar or UnitType.Shooter => spawner = new StandartUnitSpawner(dataProvider),
            UnitType.Giant or UnitType.Boozer => spawner = new SplashDamageUnitSpawner(dataProvider),
            UnitType.ZombieJumper => spawner = new ZombieJumperSpawner(dataProvider),
            _ => null
        };

        spawner.SpawnAndInitializeUnit(entityType, position);
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
