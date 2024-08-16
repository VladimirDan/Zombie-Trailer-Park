using Game.Code.Common.CoroutineRunner;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : EntityBehaviourState
{
    private IEnumerator fightEnumerator;
    public override void Enter()
    {
        fightEnumerator = entityModel.Fight();
        coroutineRunner.RunCoroutine(fightEnumerator);
    }
    public override void Exit()
    {
           coroutineRunner.StopRunningCoroutine(fightEnumerator);
    }

    public AttackState(IEntityModel _entityModel, ICoroutineRunner _coroutineRunner) : base(_entityModel, _coroutineRunner) { }
}
