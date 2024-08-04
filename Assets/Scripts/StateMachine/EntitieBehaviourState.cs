using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntitieBehaviourState
{
    protected Entitie entitieModel;
    public abstract void Enter();
    public abstract void Exit();

    public EntitieBehaviourState(Entitie _entitieModel)
    {
        entitieModel = _entitieModel;
    }
}
