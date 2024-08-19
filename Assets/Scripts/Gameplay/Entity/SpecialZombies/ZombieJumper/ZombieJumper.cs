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
    public float jumpLenght;
    public float jumpCoolDown;
    public float oponentBaseXCoord;

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
                if (((ZombieJumperModel)unitModel).isJumpPossible() && Time.time - ((ZombieJumperModel)unitModel).lastJumpTime >= ((ZombieJumperModel)unitModel).jumpCoolDown && Random.value < 0.5f)
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

    public override void Initialize()
    {
        coroutineRunner = FindObjectOfType<CoroutineRunner>();
        healthModel = GetComponent<HealthModel>();

        if (healthModel == null)
            Debug.LogWarning("HealthModel component not found on this GameObject.");

        onDestroy += DestroyObject;

        unitModel = new ZombieJumperModel(jumpLenght, jumpCoolDown, oponentBaseXCoord, CreatureSpeed,
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
