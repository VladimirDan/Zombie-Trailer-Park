using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
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

    public StateMachine(EntityBehaviourState _currentState)
    {
        currentState = _currentState;
        currentState.Enter();
    }
}
