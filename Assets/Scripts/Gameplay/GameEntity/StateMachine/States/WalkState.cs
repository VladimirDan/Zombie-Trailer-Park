using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Code.Common.CoroutineRunner;
using Assets.Scripts.Gameplay.Entity.StateMachine;
using CreaturesData;

public class WalkState : UnitBehaviourState
{
    private readonly int isWalkingHash = Animator.StringToHash("isWalking");
    private readonly int walkSpeedHash = Animator.StringToHash("WalkSpeed");
    
    public override void Enter()
    {
        if (unitModel.Animator != null)
        {
            unitModel.Animator.SetFloat(walkSpeedHash, unitModel.CreatureSpeed);
            unitModel.Animator.SetBool(isWalkingHash, true);
        }
        unitModel.Walk(unitModel.CreatureSpeed, unitModel.CreatureHorizontalMovementDirection);
    }

    public override void Exit()
    {
        unitModel.Walk(exitSpeed, unitModel.CreatureHorizontalMovementDirection);
        if (unitModel.Animator != null)
        {
            unitModel.Animator.SetBool(isWalkingHash, false);
        }
    }
    public WalkState(IUnitModel _unitModel, ICoroutineRunner _coroutineRunner) : base(_unitModel, _coroutineRunner) { }
}
