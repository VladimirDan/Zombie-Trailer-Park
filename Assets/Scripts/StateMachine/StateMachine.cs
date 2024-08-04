using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private Entitie entitieModel;
    private EntitieBehaviourState currentState;

    public void CheckoutCurrentState()
    {
        if (entitieModel.isEnemyInAttackRange())
        {
            ChangeCurrentState(new AttackState(entitieModel));
        }

        else
        {
            ChangeCurrentState(new WalkState(entitieModel));
        }
    }

    public void ChangeCurrentState(EntitieBehaviourState nextState)
    {
        if (currentState.GetType() != nextState.GetType())
        {
            Debug.Log(currentState.GetType() + " => " + nextState.GetType());
            currentState.Exit();
            currentState = nextState;
            currentState.Enter();
        }
    }

    public StateMachine(Entitie _entitieModel, EntitieBehaviourState _currentState)
    {
        entitieModel = _entitieModel;
        currentState = _currentState;
        currentState.Enter();
    }
}
