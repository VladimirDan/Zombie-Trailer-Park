using Assets.Scripts.StateMachine.States;
using UnityEngine;
using Services;

public class Unit : Entity
{
    public IUnitModel unitModel;

    public UnitType unitType;
    public float CreatureSpeed;
    public float CreatureHorizontalMovementDirection;
    public float AttackRange;
    public float AttackDamage;
    public float AttackSpeed;
    public LayerMask OpponentLayer;

    public float YeeHawPointsDrop;
    public PlayerBankModel playerBank;
    
    public override void Die()
    {
        base.Die();
        playerBank.AddYeeHawPoints(YeeHawPointsDrop);

        if (gameObject.layer == LayerMask.NameToLayer("Villager"))
        {
            playerBank.ReduceArmyCapacity(dataProvider.GetUnitCapacity(unitType));
        }
    }
    
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
    
    public virtual void setUpEntity(DataProvider dataProvider, PlayerBankModel playerBank)
    {
        base.setUpEntity(dataProvider);

        this.playerBank = playerBank;
        
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

    public virtual void Initialize(DataProvider dataProvider, PlayerBankModel playerBank)
    {
        setUpEntity(dataProvider, playerBank);
        this.enabled = true; 
    }
}

