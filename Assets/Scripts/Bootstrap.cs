using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] public GameObject coroutineRunnerPrefab;
    [SerializeField] public GameObject VillageMainBuildingPrefab;
    [SerializeField] public GameObject ZombieMainBuildingPrefab;
    private CoroutineRunner coroutineRunner;
    private MainBuildingEntity villageMainBuilding;
    private MainBuildingEntity zombieMainBuilding;

    void Awake()
    {
        GameObject runnerObject = Instantiate(coroutineRunnerPrefab);
        coroutineRunner = runnerObject.GetComponent<CoroutineRunner>();
        DontDestroyOnLoad(runnerObject);

        GameObject playerBase = Instantiate(VillageMainBuildingPrefab);
        villageMainBuilding = playerBase.GetComponent<MainBuildingEntity>();
        villageMainBuilding.Initialize();

        GameObject enemyBase = Instantiate(ZombieMainBuildingPrefab);
        zombieMainBuilding = enemyBase.GetComponent<MainBuildingEntity>();
        zombieMainBuilding.Initialize();
    }

    public CoroutineRunner GetCoroutineRunner()
    {
        return coroutineRunner;
    }
}
