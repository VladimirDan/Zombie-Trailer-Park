using UnityEngine;
using Game.Code.Common.CoroutineRunner;
using Services;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] public GameObject coroutineRunnerPrefab;
    [SerializeField] public GameObject dataProviderPrefab;
    
    [SerializeField] public GameObject villageMainBuildingPrefab;
    [SerializeField] public GameObject zombieMainBuildingPrefab;
    
    private CoroutineRunner coroutineRunner;
    private DataProvider dataProvider;
    
    private GameObject playerManager;
    private PlayerBankModel playerBank;
    
    private GameObject gameControllerObject;
    private GameController gameController;
    
    private MainBuildingEntity villageMainBuilding;
    private MainBuildingEntity zombieMainBuilding;

    private SpawnManager villagersSpawner;
    private SpawnManager zombieSpawner;

    private SummonController summonController;

    void Awake()
    {
        GameObject coroutineRunnerObject = Instantiate(coroutineRunnerPrefab);
        coroutineRunner = coroutineRunnerObject.GetComponent<CoroutineRunner>();
        DontDestroyOnLoad(coroutineRunnerObject);
        
        GameObject dataProviderObject = Instantiate(dataProviderPrefab);
        dataProvider = dataProviderObject.GetComponent<DataProvider>();
        dataProvider.Initialize();
        DontDestroyOnLoad(dataProviderObject);
        
        GameObject playerBase = Instantiate(villageMainBuildingPrefab);
        villageMainBuilding = playerBase.GetComponent<MainBuildingEntity>();
        villageMainBuilding.Initialize();

        villagersSpawner = playerBase.GetComponent<SpawnManager>();
        villagersSpawner.Initialize(coroutineRunner, dataProvider);
        
        summonController = playerBase.GetComponent<SummonController>();
        summonController.Initialize(villagersSpawner);

        GameObject enemyBase = Instantiate(zombieMainBuildingPrefab);
        zombieMainBuilding = enemyBase.GetComponent<MainBuildingEntity>();
        zombieMainBuilding.Initialize();

        zombieSpawner = enemyBase.GetComponent<SpawnManager>();
        zombieSpawner.Initialize(coroutineRunner, dataProvider);
        
        gameControllerObject = GameObject.Find("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
        gameController.Initialize(coroutineRunner, dataProvider, summonController);
        
        
    }

    public CoroutineRunner GetCoroutineRunner()
    {
        return coroutineRunner;
    }
}
