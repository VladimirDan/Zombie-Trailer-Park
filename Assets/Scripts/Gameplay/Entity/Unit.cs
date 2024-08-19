using Assets.Scripts.StateMachine.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HealthModel;
using Game.Code.Common.CoroutineRunner;
using CreaturesData;

public class Unit : Entity
{
    public IUnitModel unitModel;

    public float CreatureSpeed;
    public float CreatureHorizontalMovementDirection;
    public float AttackRange;
    public float AttackDamage;
    public float AttackSpeed;
    public LayerMask OpponentLayer;
    public Coroutine attackCoroutine;

    public override void CheckoutCurrentState()
    {
        if (!healthModel.isAlive())
        {
            stateMachine.ChangeCurrentState(new DeathState(coroutineRunner));
            Die();
        }

        else if (unitModel.isEnemyInAttackRange())
        {
            stateMachine.ChangeCurrentState(new AttackState(unitModel, coroutineRunner));
        }

        else
        {
            stateMachine.ChangeCurrentState(new WalkState(unitModel, coroutineRunner));
        }
    }

    public override void Initialize()
    {
        coroutineRunner = FindObjectOfType<CoroutineRunner>();
        healthModel = GetComponent<HealthModel>();

        if (healthModel == null)
            Debug.LogWarning("HealthModel component not found on this GameObject.");

        onDestroy += DestroyObject;

        unitModel = new UnitModel(CreatureSpeed,
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

