using Assets.Scripts.StateMachine.States;
using Gameplay.GameEntity.StateMachine.States;
using Services.LevelStatisticsManager;
using UnityEngine;
using Services;
using Enums;
using Gameplay.GameEntity.Entity;

public class Unit : Entity<UnitType>
{
    public UnitModel unitModel;

    public UnitType unitType;
    public float CreatureSpeed;
    public float CreatureHorizontalMovementDirection;
    public float AttackRange;
    public float AttackDamage;
    public float AttackCooldown;
    public LayerMask OpponentLayer;
    public LayerMask OpponentBaseLayer;
    public Animator animator;

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
        if (!healthModel.IsAlive())
        {
            stateMachine.ChangeCurrentState(new UnitDeathState(coroutineRunner, entityModel));
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
    
    public virtual void setUpEntity(DataProvider dataProvider, LevelStatisticsManager levelStatisticsManager, PlayerBankModel playerBank, AudioManager audioManager)
    {
        base.SetUpEntity(dataProvider, levelStatisticsManager, audioManager);
        this.animator = GetComponent<Animator>();
        this.playerBank = playerBank;
        
        unitModel = GetComponent<UnitModel>();

        unitModel.EntityType = unitType;
        unitModel.CreatureSpeed = CreatureSpeed;
        unitModel.CreatureHorizontalMovementDirection = CreatureHorizontalMovementDirection;
        unitModel.AttackRange = AttackRange;
        unitModel.AttackDamage = AttackDamage;
        unitModel.AttackCooldown = AttackCooldown;
        unitModel.OpponentLayer = OpponentLayer;
        unitModel.OpponentBaseLayer = OpponentBaseLayer;
        unitModel.EntityRigidbody = GetComponent<Rigidbody>();
        unitModel.EntityTransform = GetComponent<Transform>();
        unitModel.Animator = animator;
        unitModel.AudioManager = audioManager;
        
        stateMachine = new StateMachine(new UnitIdleState(unitModel, coroutineRunner));
    }

    public virtual void Initialize(DataProvider dataProvider, LevelStatisticsManager levelStatisticsManager, PlayerBankModel playerBank, AudioManager audioManager)
    {
        setUpEntity(dataProvider, levelStatisticsManager, playerBank, audioManager);
        onDestroy += HandleUnitDeath;
        this.enabled = true; 
    }
    
    protected virtual void HandleUnitDeath()
    {
        levelStatisticsManager.UpdateUnitsKilledStatistic(unitType);
    }
}

