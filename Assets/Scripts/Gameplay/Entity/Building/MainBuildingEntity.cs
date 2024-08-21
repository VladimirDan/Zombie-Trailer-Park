using Assets.Scripts.StateMachine.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HealthModel;
using Game.Code.Common.CoroutineRunner;

public class MainBuildingEntity : Entity
{
    public override void CheckoutCurrentState()
    {
        if (!healthModel.isAlive())
        {
            stateMachine.ChangeCurrentState(new DeathState(coroutineRunner));
            Die();
        }
    }

    private void HandleMainBuildingDestruction()
    {
        coroutineRunner.Dispose();
        Time.timeScale = 0f;
        Debug.Log("Level end");
    }

    public override void setUpEntity()
    {
        base.setUpEntity();
        onDestroy += HandleMainBuildingDestruction;
    }
}
