using UnityEngine;
using Game.Code.Common.CoroutineRunner;
using Services;
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
    
    private MainBuildingEntity villageMainBuilding;
    private MainBuildingEntity zombieMainBuilding;

    private SpawnManager villagersSpawner;
    private SpawnManager zombieSpawner;

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
        villageMainBuilding = playerBase.GetComponent<MainBuildingEntity>();
        villageMainBuilding.Initialize(dataProvider);

        villagersSpawner = playerBase.GetComponent<SpawnManager>();
        villagersSpawner.Initialize(coroutineRunner, dataProvider, playerBankModel);

        GameObject enemyBase = Instantiate(zombieMainBuildingPrefab);
        zombieMainBuilding = enemyBase.GetComponent<MainBuildingEntity>();
        zombieMainBuilding.Initialize(dataProvider);

        zombieSpawner = enemyBase.GetComponent<SpawnManager>();
        zombieSpawner.Initialize(coroutineRunner, dataProvider, playerBankModel);
        
        summonManager = new SummonManager(dataProvider, villagersSpawner, uiManager, playerBankModel);
        
        gameControllerObject = GameObject.Find("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
        gameController.Initialize(coroutineRunner, dataProvider, summonManager, uiManager, playerBankModel);
    }

    public CoroutineRunner GetCoroutineRunner()
    {
        return coroutineRunner;
    }
}
