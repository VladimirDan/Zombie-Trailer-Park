using Assets.Scripts.StateMachine.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HealthModel;
using Game.Code.Common.CoroutineRunner;
using CreaturesData;
using Assets.Scripts.Gameplay.Entity.StateMachine.States;


public class SplashDamageUnit : Unit
{
    public int maxSplashAttackTargets;

    public override void setUpEntity()
    {
        base.setUpEntity();

        ((SplashDamageUnitModel)unitModel).maxSplashAttackTargets = maxSplashAttackTargets;
    }
}

