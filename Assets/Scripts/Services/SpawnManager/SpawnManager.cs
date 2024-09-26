using Assets.Scripts.Services.SpawnManager.Factories;
using Game.Code.Common.CoroutineRunner;
using System.Collections;
using UnityEngine;
using Services;
using LevelsParameters;

public class SpawnManager : MonoBehaviour
{
    CoroutineRunner coroutineRunner;
    DataProvider dataProvider;
    public Vector3 baseSpawnPosition;
    private Quaternion baseRotation = Quaternion.Euler(0, 0, 0);
    private float maxDistanceBetweenRows = 5;

    [SerializeField] public UnitsSpawnTimings unitsSpawnCycleParameters;

    public void Initialize(CoroutineRunner coroutineRunner, DataProvider dataProvider)
    {
        this.coroutineRunner = coroutineRunner;
        this.dataProvider = dataProvider;

        OrderUnitSpawn();
    }

    public void OrderUnitSpawn()
    {
        foreach (var unit in unitsSpawnCycleParameters.unitsSpawnTimings)
        {
            foreach (UnitSpawnCycleParameters spawnParameters in unit.unitSpawnCycleParameters)
            {
                coroutineRunner.RunCoroutine(startSpawnCycle(baseSpawnPosition, spawnParameters.timing, unit.unitType, 
                    spawnParameters.spawnCooldown, spawnParameters.unitCount));
            }
        }
    }

    public IEnumerator startSpawnCycle(Vector3 baseSpawnPosition, float timing, UnitType unitType, float unitSpawnCooldown, int unitsCount)
    {
        yield return new WaitForSeconds(timing);

        for (int i = 0; i < unitsCount; i++)
        {
            yield return new WaitForSeconds(unitSpawnCooldown);
            SpawnUnitOnRandomRow(unitType, baseSpawnPosition);
        }
    }

    public void SpawnUnitOnRandomRow(UnitType entityType, Vector3 baseSpawnPosition)
    {
        Vector3 spawnPosition = GenerateUnitSpawnPosition(baseSpawnPosition);
        SpawnUnit(entityType, spawnPosition);
    }

    public void SpawnUnit(UnitType unitType, Vector3 position)
    {
        UnitSpawner spawner = unitType switch
        {
            UnitType.Zombie or UnitType.Banshee or UnitType.Digger
            or UnitType.Cleric or UnitType.SurvivalistCar or UnitType.Shooter => spawner = new StandartUnitSpawner(dataProvider),
            UnitType.Giant or UnitType.Boozer => spawner = new SplashDamageUnitSpawner(dataProvider),
            UnitType.ZombieJumper => spawner = new ZombieJumperSpawner(dataProvider),
            _ => null
        };

        spawner.SpawnAndInitializeUnit(unitType, position);
    }

    public Vector3 GenerateUnitSpawnPosition(Vector3 baseSpawnPosition)
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
