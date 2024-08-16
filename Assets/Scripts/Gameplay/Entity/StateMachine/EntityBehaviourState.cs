using Game.Code.Common.CoroutineRunner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBehaviourState
{
    protected IEntityModel entityModel;
    protected ICoroutineRunner coroutineRunner;
    public abstract void Enter();
    public abstract void Exit();

    public EntityBehaviourState(IEntityModel _entityModel, ICoroutineRunner _coroutineRunner)
    {
        entityModel = _entityModel;
        coroutineRunner = _coroutineRunner;
    }
}
