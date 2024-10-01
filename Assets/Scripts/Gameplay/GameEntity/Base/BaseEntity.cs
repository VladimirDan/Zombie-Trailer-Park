using Assets.Scripts.StateMachine.States;
using UnityEngine;
using Services;

public class BaseEntity : Entity
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

    public override void setUpEntity(DataProvider dataProvider)
    {
        base.setUpEntity(dataProvider);
        onDestroy += HandleMainBuildingDestruction;
    }
}
