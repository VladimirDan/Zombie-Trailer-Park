using Services;
using UnityEngine;

public abstract class EntitySpawner
{
    protected DataProvider dataProvider;
    public virtual void SpawnEntity(Vector3 spawnPosition) { }

    public EntitySpawner(DataProvider dataProvider)
    {
        this.dataProvider = dataProvider;
    }
}
