using UI;
using UnityEngine;
using Services.LevelManager;
using Game.Code.Common.CoroutineRunner;

namespace Bootstraps
{
    public class MainMenuBootstrap : MonoBehaviour
    {
        [SerializeField] public GameObject coroutineRunnerPrefab;
        private CoroutineRunner coroutineRunner;
        [SerializeField] private MainMenuUIManager menuUIManager;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private GameObject musicObjectPrefab;
        private GameObject musicObject;
        
        
        void Awake()
        {
            coroutineRunner = FindObjectOfType<CoroutineRunner>();
            if (coroutineRunner == null)
            {
                GameObject coroutineRunnerObject = Instantiate(coroutineRunnerPrefab);
                coroutineRunner = coroutineRunnerObject.GetComponent<CoroutineRunner>();
                DontDestroyOnLoad(coroutineRunnerObject);
            }
            
            levelManager.Initialize(coroutineRunner);
            menuUIManager.Initialize();

            if (GameObject.Find("Music") == null)
            {
                musicObject = Instantiate(musicObjectPrefab);
                musicObject.name = musicObjectPrefab.name;
                DontDestroyOnLoad(musicObject);
            }
        }
    }
}