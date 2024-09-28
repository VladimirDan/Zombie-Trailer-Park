using Assets.Scripts.StateMachine.States;
using UnityEngine;

public class Unit : Entity
{
    public IUnitModel unitModel;

    public float CreatureSpeed;
    public float CreatureHorizontalMovementDirection;
    public float AttackRange;
    public float AttackDamage;
    public float AttackSpeed;
    public LayerMask OpponentLayer;

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

