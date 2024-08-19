using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Code.Common.CoroutineRunner;
using Assets.Scripts.Gameplay.Entity.StateMachine;
using CreaturesData;

public class WalkState : UnitBehaviourState
{
    public override void Enter()
    {
        unitModel.Walk(unitModel.CreatureSpeed, unitModel.CreatureHorizontalMovementDirection);
    }

    public override void Exit()
    {
        unitModel.Walk(exitSpeed, unitModel.CreatureHorizontalMovementDirection);
    }
    public WalkState(IUnitModel _unitModel, ICoroutineRunner _coroutineRunner) : base(_unitModel, _coroutineRunner) { }
}
