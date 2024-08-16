using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] public GameObject coroutineRunnerPrefab;
    private CoroutineRunner coroutineRunner;

    void Awake()
    {
        GameObject runnerObject = Instantiate(coroutineRunnerPrefab);
        coroutineRunner = runnerObject.GetComponent<CoroutineRunner>();

        DontDestroyOnLoad(runnerObject);
    }

    public CoroutineRunner GetCoroutineRunner()
    {
        return coroutineRunner;
    }
}
