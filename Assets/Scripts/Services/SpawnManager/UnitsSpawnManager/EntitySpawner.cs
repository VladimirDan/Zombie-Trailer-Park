using Services;
using UnityEngine;
using Services.LevelStatisticsManager;

public abstract class EntitySpawner
{
    protected DataProvider dataProvider;
    protected LevelStatisticsManager levelStatisticsManager;
    public virtual void SpawnEntity(Vector3 spawnPosition) { }

    public EntitySpawner(DataProvider dataProvider, LevelStatisticsManager levelStatisticsManager)
    {
        this.dataProvider = dataProvider;
        this.levelStatisticsManager = levelStatisticsManager;
    }
}
