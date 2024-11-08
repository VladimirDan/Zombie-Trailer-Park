using Assets.Scripts.Gameplay.Entity.StateMachine;
using Game.Code.Common.CoroutineRunner;
using System.Collections;
using System.Collections.Generic;
using Enums;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class AttackState : UnitBehaviourState
{
    private readonly int isAttackingHash = Animator.StringToHash("isAttacking");
    
    private IEnumerator fightEnumerator;
    
    public override void Enter()
    {
        fightEnumerator = unitModel.Fight();
        coroutineRunner.RunCoroutine(fightEnumerator);
        unitModel.AudioManager.AddUnitAttackSoundArtist(unitModel.EntityType, unitModel.AttackCooldown);
        
        if (unitModel.Animator != null)
        {
            unitModel.Animator.SetBool(isAttackingHash, true);
            float originalAnimationLength = unitModel.Animator.GetCurrentAnimatorStateInfo(0).length;
            Debug.Log(unitModel.EntityType + " original animation length " + originalAnimationLength);
            Debug.Log("multiplyer " + unitModel.AttackCooldown / originalAnimationLength);
            //unitModel.Animator.SetFloat("AttackSpeed", unitModel.AttackCooldown / originalAnimationLength);
            unitModel.Animator.SetFloat("AttackSpeed", 2);
        }
    }
    public override void Exit()
    {
           coroutineRunner.StopRunningCoroutine(fightEnumerator);
           unitModel.AudioManager.RemoveAttackSoundArtist(unitModel.EntityType);
           
           if (unitModel.Animator != null)
           {
               unitModel.Animator.SetBool(isAttackingHash, false);
           }
    }

    public AttackState(IUnitModel _unitModel, ICoroutineRunner _coroutineRunner) : base(_unitModel, _coroutineRunner) { }
}
