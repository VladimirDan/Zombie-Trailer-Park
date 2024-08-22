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

    public override void setUpEntity()
    {
        base.setUpEntity();

        unitModel = GetComponent<UnitModel>();

        unitModel.CreatureSpeed = CreatureSpeed;
        unitModel.CreatureHorizontalMovementDirection = CreatureHorizontalMovementDirection;
        unitModel.AttackRange = AttackRange;
        unitModel.AttackDamage = AttackDamage;
        unitModel.AttackSpeed = AttackSpeed;
        unitModel.OpponentLayer = OpponentLayer;
        unitModel.EntityRigidbody = GetComponent<Rigidbody>();
        unitModel.EntityTransform = GetComponent<Transform>();
    }
}

