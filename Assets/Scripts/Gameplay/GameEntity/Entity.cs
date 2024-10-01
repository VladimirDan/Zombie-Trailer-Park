using Assets.Scripts.StateMachine.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HealthModel;
using Game.Code.Common.CoroutineRunner;
using CreaturesData;
using Services;

public class Entity : MonoBehaviour
{
    protected HealthModel healthModel;
    public CoroutineRunner coroutineRunner;
    protected StateMachine stateMachine;
    protected DataProvider dataProvider;

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
            stateMachine.ChangeCurrentState(new DeathState(coroutineRunner));
            Die();
        }
    }

    public virtual void setUpEntity(DataProvider dataProvider)
    {
        coroutineRunner = FindObjectOfType<CoroutineRunner>();
        healthModel = GetComponent<HealthModel>();
        this.dataProvider = dataProvider;

        if (healthModel == null)
            Debug.LogWarning("HealthModel component not found on this GameObject.");

        onDestroy += DestroyObject;

        stateMachine = new StateMachine(new AfkState(coroutineRunner));
    }

    public virtual void Initialize(DataProvider dataProvider)
    {
        setUpEntity(dataProvider);
        this.enabled = true;
    }

    void Update()
    {
        CheckoutCurrentState();
    }
}
