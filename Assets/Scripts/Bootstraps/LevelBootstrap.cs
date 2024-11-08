using UnityEngine;
using Game.Code.Common.CoroutineRunner;
using Gameplay.GameEntity.Base;
using Services;
using Services.LevelStatisticsManager;
using Services.SpawnManager;
using Services.Timer;
using UI;

public class LevelBootstrap : MonoBehaviour
{
    [SerializeField] public GameObject coroutineRunnerPrefab;
    
    [SerializeField] public GameObject villageMainBuildingPrefab;
    [SerializeField] public GameObject zombieMainBuildingPrefab;
    
    [SerializeField] public TimerManager timerManager;
    [SerializeField] private BuildingImageManager buildingImageManager;

    [SerializeField] private AudioManager audioManager;
    
    private CoroutineRunner coroutineRunner;
    private DataProvider dataProvider;
    
    private GameObject uiManagerObject;
    private LevelUIManager levelUIManager;
    
    private GameObject gameControllerObject;
    private GameController gameController;
    
    private PlayerBase villageBase;
    private EnemyBase zombieBase;

    private UnitsSpawnManager villagersSpawner;
    private UnitsSpawnManager zombieSpawner;
    private BuildingsSpawnManager buildingsSpawner;
    
    private PlayerBankModel playerBankModel;
    
    private SummonManager summonManager;
    
    private HealthModel playerBaseHealth;
    private HealthModel zombieBaseHealth;

    private LevelStatisticsManager levelStatisticsManager;

    void Awake()
    {
        levelStatisticsManager = new LevelStatisticsManager();

        coroutineRunner = FindObjectOfType<CoroutineRunner>();
        if (coroutineRunner == null)
        {
            GameObject coroutineRunnerObject = Instantiate(coroutineRunnerPrefab);
            coroutineRunner = coroutineRunnerObject.GetComponent<CoroutineRunner>();
            DontDestroyOnLoad(coroutineRunnerObject);
        }

        GameObject dataProviderObject = GameObject.Find("DataProvider");
        dataProvider = dataProviderObject.GetComponent<DataProvider>();
        dataProvider.Initialize();
        
        audioManager.Initialize(coroutineRunner);
        timerManager.Initialize();
        
        uiManagerObject = GameObject.Find("UIManager");
        levelUIManager = uiManagerObject.GetComponent<LevelUIManager>();
        levelUIManager.Initialize(dataProvider, levelStatisticsManager);
        
        GameObject playerBase = Instantiate(villageMainBuildingPrefab);
        villageBase = playerBase.GetComponent<PlayerBase>();
        villageBase.Initialize(dataProvider, timerManager.GetTimer(), levelStatisticsManager, levelUIManager, audioManager);

        GameObject enemyBase = Instantiate(zombieMainBuildingPrefab);
        zombieBase = enemyBase.GetComponent<EnemyBase>();
        zombieBase.Initialize(dataProvider, timerManager.GetTimer(), levelStatisticsManager, levelUIManager, audioManager);

        playerBaseHealth = playerBase.GetComponent<HealthModel>();
        playerBaseHealth.SetHealth(dataProvider.GetPlayerAndZombieBasesParameters().playerBase.healthPoints);
        
        zombieBaseHealth = zombieBase.GetComponent<HealthModel>();
        zombieBaseHealth.SetHealth(dataProvider.GetPlayerAndZombieBasesParameters().zombieBase.healthPoints);

        playerBankModel = new PlayerBankModel(coroutineRunner, dataProvider, levelUIManager);
        playerBankModel.Initialize();
        
        villagersSpawner = playerBase.GetComponent<UnitsSpawnManager>();
        villagersSpawner.Initialize(coroutineRunner, dataProvider, playerBankModel, null, levelStatisticsManager, audioManager);
        
        zombieSpawner = enemyBase.GetComponent<UnitsSpawnManager>();
        zombieSpawner.Initialize(coroutineRunner, dataProvider, playerBankModel, dataProvider.GetZombiesSpawnCycleParameters(), levelStatisticsManager, audioManager);

        buildingsSpawner = new BuildingsSpawnManager(dataProvider, playerBankModel, buildingImageManager);
        
        summonManager = new SummonManager(dataProvider, villagersSpawner, buildingsSpawner, levelUIManager, playerBankModel, audioManager);
        
        gameControllerObject = GameObject.Find("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
        gameController.Initialize(coroutineRunner, dataProvider, summonManager, levelUIManager, audioManager);
    }

    public CoroutineRunner GetCoroutineRunner()
    {
        return coroutineRunner;
    }
}
