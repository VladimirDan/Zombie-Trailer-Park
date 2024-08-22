using Assets.Scripts.StateMachine.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HealthModel;
using Game.Code.Common.CoroutineRunner;
using CreaturesData;
using Assets.Scripts.Gameplay.Entity.StateMachine.States;

public class ZombieJumper : Unit
{
    public float jumpLength;
    public float jumpCooldown;
    public float opponentBaseXCoord;

    public override void CheckoutCurrentState()
    {
        if (!healthModel.isAlive())
        {
            stateMachine.ChangeCurrentState(new DeathState(coroutineRunner));
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

    public override void setUpEntity()
    {
        base.setUpEntity();

        ((ZombieJumperModel)unitModel).jumpLength = jumpLength;
        ((ZombieJumperModel)unitModel).jumpCooldown = jumpCooldown;
        ((ZombieJumperModel)unitModel).opponentBaseXCoord = opponentBaseXCoord;
    }
}
