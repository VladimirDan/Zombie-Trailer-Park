using Assets.Scripts.StateMachine.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HealthModel;
using Game.Code.Common.CoroutineRunner;
using CreaturesData;

public class Entity : MonoBehaviour
{
    protected HealthModel healthModel;
    public CoroutineRunner coroutineRunner;
    protected StateMachine stateMachine;

    public delegate void OnDestroyEvent();
    public event OnDestroyEvent onDestroy;

    public bool isInitialized = false;

    public void Die()
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
        if (!healthModel.isAlive())
        {
            stateMachine.ChangeCurrentState(new DeathState(coroutineRunner));
            Die();
        }
    }

    public virtual void Initialize()
    {
        coroutineRunner = FindObjectOfType<CoroutineRunner>();
        healthModel = GetComponent<HealthModel>();

        if (healthModel == null)
            Debug.LogWarning("HealthModel component not found on this GameObject.");

        onDestroy += DestroyObject;

        stateMachine = new StateMachine(new AfkState(coroutineRunner));
    }

    void Update()
    {
        if (!isInitialized)
            return;

        CheckoutCurrentState();
    }
}
