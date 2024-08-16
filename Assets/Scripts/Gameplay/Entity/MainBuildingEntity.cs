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
            stateMachine.ChangeCurrentState(new DeathState(entityModel, coroutineRunner));
            Die();
        }
    }

    private void HandleMainBuildingDestruction()
    {
        coroutineRunner.Dispose();
        Time.timeScale = 0f;
        Debug.Log("Level end");
    }
    public override void Start()
    {
        coroutineRunner = FindObjectOfType<CoroutineRunner>();
        healthModel = GetComponent<HealthModel>();

        if (healthModel == null)
            Debug.LogWarning("HealthModel component not found on this GameObject.");

        onDestroy += DestroyObject;
        onDestroy += HandleMainBuildingDestruction;

        entityModel = new EntityModel(CreatureSpeed, CreatureHorizontalMovementDirection, AttackRange,
                                        AttackDamage, AttackSpeed, OpponentLayer,
                                        GetComponent<Rigidbody>(), this.transform);

        stateMachine = new StateMachine(entityModel, new AfkState(entityModel, coroutineRunner));
    }
}
