using Assets.Scripts.Gameplay.Entity.StateMachine;
using Game.Code.Common.CoroutineRunner;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : UnitBehaviourState
{
    private IEnumerator fightEnumerator;
    public override void Enter()
    {
        fightEnumerator = unitModel.Fight();
        coroutineRunner.RunCoroutine(fightEnumerator);
    }
    public override void Exit()
    {
           coroutineRunner.StopRunningCoroutine(fightEnumerator);
    }

    public AttackState(IUnitModel _unitModel, ICoroutineRunner _coroutineRunner) : base(_unitModel, _coroutineRunner) { }
}
