using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Code.Common.CoroutineRunner;

public class WalkState : EntityBehaviourState
{
    public float exitSpeed = 0f;

    public override void Enter()
    {
        entityModel.Walk(entityModel.CreatureSpeed, entityModel.CreatureHorizontalMovementDirection);
    }

    public override void Exit()
    {
        entityModel.Walk(exitSpeed, entityModel.CreatureHorizontalMovementDirection);
    }
    public WalkState(IEntityModel _entityModel, ICoroutineRunner _coroutineRunner) : base(_entityModel, _coroutineRunner) { }
}
