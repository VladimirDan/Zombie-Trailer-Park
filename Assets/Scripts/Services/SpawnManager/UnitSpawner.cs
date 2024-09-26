using CreaturesData;
using UnityEngine;
using Services;

public class UnitSpawner : EntitySpawner
{
    public void SpawnAndInitializeUnit(UnitType entityType, Vector3 spawnPosition)
    {
        Unit unitObject = SpawnUnit(entityType, spawnPosition);
        unitObject.Initialize();
    }
    public virtual Unit SpawnUnit(UnitType entityType, Vector3 spawnPosition)
    {
        GameObject prefab = dataProvider.GetUnitPrefab(entityType);
        UnitParametersData unitData = (UnitParametersData)dataProvider.GetUnitData(entityType);

        GameObject unit = UnityEngine.Object.Instantiate(prefab, spawnPosition, Quaternion.Euler(prefab.transform.rotation.eulerAngles));

        unit.GetComponent<HealthModel>().setHealth(unitData.health);

        Unit unitObject = unit.GetComponent<Unit>();
        SetParameters(unitObject, unitData);

        return unitObject;
    }

    public virtual void SetParameters(Unit unitObject, UnitParametersData unitData)
    {
    }

    public UnitSpawner(DataProvider dataProvider) : base(dataProvider) { }
}

