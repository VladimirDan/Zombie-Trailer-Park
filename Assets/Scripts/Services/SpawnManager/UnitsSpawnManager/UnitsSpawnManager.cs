using Assets.Scripts.Services.SpawnManager.Factories;
using Game.Code.Common.CoroutineRunner;
using System.Collections;
using UnityEngine;
using Services;
using GameParameters;
using Enums;
using Services.LevelStatisticsManager;

public class UnitsSpawnManager : MonoBehaviour
{
    CoroutineRunner coroutineRunner;
    DataProvider dataProvider;
    private PlayerBankModel playerBankModel;
    private AudioManager audioManager;
    
    public Vector3 spawnPosition;
    private Quaternion baseRotation = Quaternion.Euler(0, 0, 0);

    [SerializeField] private Transform floorTransform;
    [SerializeField] private Vector3 spawnPositionFromSpawner;
    [SerializeField] private int rowsCount;
    private float zDistanceBetweenRows;
    private float floorWidth;

    [SerializeField] public UnitsSpawnTimings unitsSpawnCycleParameters;
    private LevelStatisticsManager levelStatisticsManager;

    public void Initialize(CoroutineRunner coroutineRunner, DataProvider dataProvider, PlayerBankModel playerBankModel,
        UnitsSpawnTimings unitsSpawnCycleParameters, LevelStatisticsManager levelStatisticsManager, AudioManager audioManager)
    {
        this.coroutineRunner = coroutineRunner;
        this.dataProvider = dataProvider;
        this.playerBankModel = playerBankModel;
        this.unitsSpawnCycleParameters = unitsSpawnCycleParameters;
        this.levelStatisticsManager = levelStatisticsManager;
        this.audioManager = audioManager;

        floorTransform = dataProvider.GetLevelFloorTransform();
        floorWidth = floorTransform.transform.localScale.z;
        zDistanceBetweenRows = floorWidth / rowsCount;
        
        spawnPosition = this.transform.localPosition;
        if (floorTransform != null)
        {
            spawnPosition.y = floorTransform.transform.localPosition.y + (floorTransform.transform.localScale.y / 2);
        }

        spawnPosition += spawnPositionFromSpawner;
        
        OrderUnitsSpawn(unitsSpawnCycleParameters);
    }

    public void OrderUnitsSpawn(UnitsSpawnTimings unitsSpawnCycleParameters)
    {
        if (unitsSpawnCycleParameters == null)
        {
            return;
        }
        
        foreach (var unit in unitsSpawnCycleParameters.unitsSpawnTimings)
        {
            foreach (UnitSpawnCycleParameters spawnParameters in unit.unitSpawnCycleParameters)
            {
                coroutineRunner.RunCoroutine(startSpawnCycle(spawnPosition, spawnParameters.timing, unit.unitType, 
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

    public void SpawnUnitOnRandomRow(UnitType unitType, Vector3 baseSpawnPosition)
    {
        Vector3 spawnPosition = GenerateUnitSpawnPosition(baseSpawnPosition);
        SpawnUnit(unitType, spawnPosition);
    }

    public void SpawnUnit(UnitType unitType, Vector3 position)
    {
        UnitSpawner spawner = unitType switch
        {
            UnitType.Zombie or UnitType.Banshee or UnitType.Digger or UnitType.Harvester
            or UnitType.SurvivalistCar or UnitType.Cleric or UnitType.Shooter => spawner = new StandartUnitSpawner(dataProvider, levelStatisticsManager, playerBankModel),
            UnitType.Giant => spawner = new SplashDamageUnitSpawner(dataProvider, levelStatisticsManager, playerBankModel),
            UnitType.ZombieJumper => spawner = new ZombieJumperSpawner(dataProvider, levelStatisticsManager, playerBankModel),
            UnitType.Boozer => spawner = new BombThrowerUnitSpawner(dataProvider, levelStatisticsManager, playerBankModel),
            UnitType.AirStrikePlane => spawner = new AirStrikePlaneSpawner(dataProvider, levelStatisticsManager, playerBankModel, coroutineRunner),
            _ => null
        };

        spawner.SpawnAndInitializeUnit(unitType, position, playerBankModel, audioManager);
    }

    public Vector3 GenerateUnitSpawnPosition(Vector3 baseSpawnPosition)
    {
        float floorMiddle = floorTransform.transform.localPosition.z;
        float zCoordinate = PickRandomLineForUnitWalkWay(floorMiddle - floorWidth / 2, floorMiddle + floorWidth / 2);
        
        return new Vector3(baseSpawnPosition.x, baseSpawnPosition.y, zCoordinate);
    }

    public float PickRandomLineForUnitWalkWay(float minValue, float maxValue)
    {
        float[] possibleValues = new float[rowsCount];
        for (int i = 0; i < possibleValues.Length; i++)
        {
            possibleValues[i] = minValue + i * zDistanceBetweenRows;
        }
        int randomIndex = Random.Range(0, possibleValues.Length);
        return possibleValues[randomIndex];
    }
}
