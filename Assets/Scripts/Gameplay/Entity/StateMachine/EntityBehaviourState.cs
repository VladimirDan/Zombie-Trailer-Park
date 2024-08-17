using Game.Code.Common.CoroutineRunner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBehaviourState
{
    protected ICoroutineRunner coroutineRunner;
    public abstract void Enter();
    public abstract void Exit();

    public EntityBehaviourState(ICoroutineRunner _coroutineRunner)
    {
        coroutineRunner = _coroutineRunner;
    }
}
