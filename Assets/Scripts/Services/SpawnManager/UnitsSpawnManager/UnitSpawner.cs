using CreaturesData;
using UnityEngine;
using Services;
using Enums;

public class UnitSpawner : EntitySpawner
{
    protected PlayerBankModel playerBankModel;
    
    public void SpawnAndInitializeUnit(UnitType entityType, Vector3 spawnPosition, PlayerBankModel playerBankModel)
    {
        Unit unitObject = SpawnUnit(entityType, spawnPosition);
        unitObject.Initialize(dataProvider, playerBankModel);
    }
    
    public virtual Unit SpawnUnit(UnitType entityType, Vector3 spawnPosition)
    {
        GameObject prefab = dataProvider.GetUnitPrefab(entityType);
        UnitParametersData unitData = (UnitParametersData)dataProvider.GetUnitData(entityType);

        float unitHeight = prefab.GetComponent<Transform>().localScale.y ;
        spawnPosition.y += (unitHeight / 2);
        
        GameObject unit = UnityEngine.Object.Instantiate(prefab, spawnPosition, Quaternion.Euler(prefab.transform.rotation.eulerAngles));

        unit.GetComponent<HealthModel>().SetHealth(unitData.health);

        Unit unitObject = unit.GetComponent<Unit>();
        SetParameters(unitObject, unitData);

        return unitObject;
    }

    public virtual void SetParameters(Unit unitObject, UnitParametersData unitData)
    {
    }

    public UnitSpawner(DataProvider dataProvider, PlayerBankModel playerBankModel) : base(dataProvider)
    {
        this.playerBankModel = playerBankModel;
    }
}

