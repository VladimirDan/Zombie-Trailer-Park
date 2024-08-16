using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private IEntityModel entityModel;
    private EntityBehaviourState currentState;

    public void ChangeCurrentState(EntityBehaviourState nextState)
    {
        if (currentState.GetType() != nextState.GetType())
        {
            //Debug.Log(currentState.GetType() + " -> " + nextState.GetType());
            currentState.Exit();
            currentState = nextState;
            currentState.Enter();
        }
    }

    public StateMachine(IEntityModel _entityModel, EntityBehaviourState _currentState)
    {
        entityModel = _entityModel;
        currentState = _currentState;
        currentState.Enter();
    }
}
