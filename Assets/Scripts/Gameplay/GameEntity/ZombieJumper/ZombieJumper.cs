using Gameplay.GameEntity.StateMachine.States;
using UnityEngine;
using Services;
using Assets.Scripts.Gameplay.Entity.StateMachine.States;
using Enums;
using Services.LevelStatisticsManager;


public class ZombieJumper : Unit
{
    public float jumpLength;
    public float jumpCooldown;
    public float opponentBaseXCoord;

    public override void CheckoutCurrentState()
    {
        if (!healthModel.IsAlive())
        {
            stateMachine.ChangeCurrentState(new UnitDeathState(coroutineRunner, entityModel));
            Die();
        }

        else if (((ZombieJumperModel)unitModel).isGrounded()) 
        {
            if (unitModel.isEnemyInAttackRange())
            {
                if (((ZombieJumperModel)unitModel).isJumpPossible() && Time.time - ((ZombieJumperModel)unitModel).lastJumpTime >= ((ZombieJumperModel)unitModel).jumpCooldown && Random.value < 0.5f)
                {
                    stateMachine.ChangeCurrentState(new JumpState((ZombieJumperModel)unitModel, coroutineRunner));
                }
                else
                {
                    stateMachine.ChangeCurrentState(new AttackState(unitModel, coroutineRunner));
                }
            }

            else if (((ZombieJumperModel)unitModel).isEnemyInJumpDistanceRange() && ((ZombieJumperModel)unitModel).isJumpPossible())
            {
                stateMachine.ChangeCurrentState(new JumpState((ZombieJumperModel)unitModel, coroutineRunner));
            }

            else
            {
                stateMachine.ChangeCurrentState(new WalkState(unitModel, coroutineRunner));
            }
        }
    }

    public override void setUpEntity(DataProvider dataProvider, LevelStatisticsManager levelStatisticsManager, PlayerBankModel playerBankModel, AudioManager audioManager)
    {
        base.setUpEntity(dataProvider, levelStatisticsManager, playerBankModel, audioManager);

        ((ZombieJumperModel)unitModel).jumpLength = jumpLength;
        ((ZombieJumperModel)unitModel).jumpCooldown = jumpCooldown;
        ((ZombieJumperModel)unitModel).opponentBaseXCoord = opponentBaseXCoord;
    }
}
