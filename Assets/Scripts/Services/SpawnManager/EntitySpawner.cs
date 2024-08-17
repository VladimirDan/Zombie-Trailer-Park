using Assets.Scripts.Services.SpawnManager;
using CreaturesData;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public abstract class EntitySpawner
{
    protected DataProvider dataProvider;
    public abstract void SpawnEntity(string entityName, Vector3 spawnPosition);

    public EntitySpawner(DataProvider dataProvider)
    {
        this.dataProvider = dataProvider;
    }
}
