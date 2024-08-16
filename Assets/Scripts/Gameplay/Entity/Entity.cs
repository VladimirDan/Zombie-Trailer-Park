using Assets.Scripts.StateMachine.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HealthModel;
using Game.Code.Common.CoroutineRunner;

public class Entity : MonoBehaviour
{
    public IEntityModel entityModel;
    protected HealthModel healthModel;
    public CoroutineRunner coroutineRunner;
    protected StateMachine stateMachine;
    [SerializeField] public float CreatureSpeed;
    [SerializeField] public float CreatureHorizontalMovementDirection;
    [SerializeField] public float AttackRange;
    [SerializeField] public float AttackDamage;
    [SerializeField] public float AttackSpeed;
    [SerializeField] public LayerMask OpponentLayer;
    public Coroutine attackCoroutine;


    public delegate void OnDestroyEvent();
    public event OnDestroyEvent onDestroy;

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
            stateMachine.ChangeCurrentState(new DeathState(entityModel, coroutineRunner));
            Die();
        }

        else if(entityModel.isEnemyInAttackRange())
        {
            stateMachine.ChangeCurrentState(new AttackState(entityModel, coroutineRunner));
        }

        else
        {
            stateMachine.ChangeCurrentState(new WalkState(entityModel, coroutineRunner));
        }
    }

    public virtual void Start()
    {
        coroutineRunner = FindObjectOfType<CoroutineRunner>();
        healthModel = GetComponent<HealthModel>();

        if (healthModel == null)
            Debug.LogWarning("HealthModel component not found on this GameObject.");

        onDestroy += DestroyObject;

        entityModel = new EntityModel(CreatureSpeed, CreatureHorizontalMovementDirection, AttackRange,
                                        AttackDamage, AttackSpeed, OpponentLayer,
                                        GetComponent<Rigidbody>(), this.transform);

        stateMachine = new StateMachine(entityModel , new AfkState(entityModel, coroutineRunner));
    }

    void Update()
    {
        CheckoutCurrentState();
    }
}
