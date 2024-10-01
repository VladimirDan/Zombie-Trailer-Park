using UnityEngine;
using Game.Code.Common.CoroutineRunner;
using Services;
using Services.SpawnManager;
using UI;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] public GameObject coroutineRunnerPrefab;
    [SerializeField] public GameObject dataProviderPrefab;
    
    [SerializeField] public GameObject villageMainBuildingPrefab;
    [SerializeField] public GameObject zombieMainBuildingPrefab;
    
    private CoroutineRunner coroutineRunner;
    private DataProvider dataProvider;
    
    private GameObject uiManagerObject;
    private UIManager uiManager;
    
    private GameObject gameControllerObject;
    private GameController gameController;
    
    private BaseEntity villageBase;
    private BaseEntity zombieBase;

    private UnitsSpawnManager villagersSpawner;
    private UnitsSpawnManager zombieSpawner;
    private BuildingsSpawnManager buildingsSpawner;
    
    private PlayerBankModel playerBankModel;
    
    private SummonManager summonManager;

    void Awake()
    {
        GameObject coroutineRunnerObject = Instantiate(coroutineRunnerPrefab);
        coroutineRunner = coroutineRunnerObject.GetComponent<CoroutineRunner>();
        DontDestroyOnLoad(coroutineRunnerObject);
        
        GameObject dataProviderObject = Instantiate(dataProviderPrefab);
        dataProvider = dataProviderObject.GetComponent<DataProvider>();
        dataProvider.Initialize();
        DontDestroyOnLoad(dataProviderObject);
        
        uiManagerObject = GameObject.Find("UIManager");
        uiManager = uiManagerObject.GetComponent<UIManager>();

        playerBankModel = new PlayerBankModel(coroutineRunner, dataProvider, uiManager);
        playerBankModel.Initialize();
        
        GameObject playerBase = Instantiate(villageMainBuildingPrefab);
        villageBase = playerBase.GetComponent<BaseEntity>();
        villageBase.Initialize(dataProvider);

        villagersSpawner = playerBase.GetComponent<UnitsSpawnManager>();
        villagersSpawner.Initialize(coroutineRunner, dataProvider, playerBankModel);

        GameObject enemyBase = Instantiate(zombieMainBuildingPrefab);
        zombieBase = enemyBase.GetComponent<BaseEntity>();
        zombieBase.Initialize(dataProvider);

        zombieSpawner = enemyBase.GetComponent<UnitsSpawnManager>();
        zombieSpawner.Initialize(coroutineRunner, dataProvider, playerBankModel);

        buildingsSpawner = new BuildingsSpawnManager(dataProvider, playerBankModel);
        
        summonManager = new SummonManager(dataProvider, villagersSpawner, buildingsSpawner, uiManager, playerBankModel);
        
        gameControllerObject = GameObject.Find("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
        gameController.Initialize(coroutineRunner, dataProvider, summonManager, uiManager, playerBankModel);
    }

    public CoroutineRunner GetCoroutineRunner()
    {
        return coroutineRunner;
    }
}
