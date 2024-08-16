using Game.Code.Common.CoroutineRunner;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : EntityBehaviourState
{
    private IEnumerator attackEnumerator;
    public override void Enter()
    {
        GameObject target = entityModel.FindOpponent();

        attackEnumerator = entityModel.Attack(target);
        coroutineRunner.RunCoroutine(attackEnumerator);
    }
    public override void Exit()
    {
           coroutineRunner.StopRunningCoroutine(attackEnumerator);
    }

    public AttackState(IEntityModel _entityModel, ICoroutineRunner _coroutineRunner) : base(_entityModel, _coroutineRunner) { }
}
