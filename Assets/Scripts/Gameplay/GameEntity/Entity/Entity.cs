using Assets.Scripts.StateMachine.States;
using Services.LevelStatisticsManager;
using UnityEngine;
using UnityEngine.Animations;
using static HealthModel;
using Game.Code.Common.CoroutineRunner;
using CreaturesData;
using Gameplay.GameEntity.Entity;
using Services;

public class Entity<T> : MonoBehaviour
{
    protected HealthModel healthModel;
    public CoroutineRunner coroutineRunner;
    public StateMachine stateMachine;
    protected DataProvider dataProvider;
    protected LevelStatisticsManager levelStatisticsManager;
    protected EntityModel<T> entityModel;
    protected AudioManager audioManager;

    public delegate void OnDestroyEvent();
    public event OnDestroyEvent onDestroy;

    public virtual void Die()
    {
        this.HandleDestroy();
    }

    public void HandleDestroy()
    {
        onDestroy?.Invoke();
    }

    protected void DestroyObject() => Destroy(this.gameObject);

    public virtual void CheckoutCurrentState()
    {
        if (!healthModel.IsAlive())
        {
            stateMachine.ChangeCurrentState(new DeathState<T>(coroutineRunner, entityModel));
            Die();
        }
    }

    public virtual void SetUpEntity(DataProvider dataProvider, LevelStatisticsManager levelStatisticsManager, AudioManager audioManager)
    {
        coroutineRunner = FindObjectOfType<CoroutineRunner>();
        healthModel = GetComponent<HealthModel>();
        entityModel = GetComponent<EntityModel<T>>();
        this.dataProvider = dataProvider;
        this.levelStatisticsManager = levelStatisticsManager;
        
        entityModel.AudioManager = audioManager;

        if (healthModel == null)
            Debug.LogWarning("HealthModel component not found on this GameObject.");

        onDestroy += DestroyObject;

        stateMachine = new StateMachine(new IdleState<T>(entityModel, coroutineRunner));
    }

    public virtual void Initialize(DataProvider dataProvider, LevelStatisticsManager levelStatisticsManager, AudioManager audioManager)
    {
        SetUpEntity(dataProvider, levelStatisticsManager, audioManager);
        this.enabled = true;
    }

    void Update()
    {
        CheckoutCurrentState();
    }
}
