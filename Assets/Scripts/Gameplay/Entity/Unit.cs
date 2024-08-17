using Assets.Scripts.StateMachine.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HealthModel;
using Game.Code.Common.CoroutineRunner;
using CreaturesData;

public class Unit : Entity
{
    public IEntityModel entityModel;
    [SerializeField] public EntityParametersData EntityParameters { get; set; }

    public float CreatureSpeed;
    public float CreatureHorizontalMovementDirection;
    public float AttackRange;
    public float AttackDamage;
    public float AttackSpeed;
    [SerializeField] public LayerMask OpponentLayer;
    public Coroutine attackCoroutine;

    public override void CheckoutCurrentState()
    {
        if (!healthModel.isAlive())
        {
            stateMachine.ChangeCurrentState(new DeathState(coroutineRunner));
            Die();
        }

        else if (entityModel.isEnemyInAttackRange())
        {
            stateMachine.ChangeCurrentState(new AttackState(entityModel, coroutineRunner));
        }

        else
        {
            stateMachine.ChangeCurrentState(new WalkState(entityModel, coroutineRunner));
        }
    }

    public override void Initialize()
    {
        coroutineRunner = FindObjectOfType<CoroutineRunner>();
        healthModel = GetComponent<HealthModel>();

        if (healthModel == null)
            Debug.LogWarning("HealthModel component not found on this GameObject.");

        onDestroy += DestroyObject;

        entityModel = new EntityModel(CreatureSpeed,
            CreatureHorizontalMovementDirection,
            AttackRange,
            AttackDamage,
            AttackSpeed,
            OpponentLayer,
            GetComponent<Rigidbody>(),
            this.transform);

        stateMachine = new StateMachine(new AfkState(coroutineRunner));

        isInitialized = true;
    }
}
