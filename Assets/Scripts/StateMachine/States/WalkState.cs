using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : EntitieBehaviourState
{
    public float exitSpeed = 0f;

    public override void Enter()
    {
        entitieModel.Walk(entitieModel.CreatureSpeed, entitieModel.CreatureHorizontalMovementDirection);
    }

    public override void Exit()
    {
        entitieModel.Walk(exitSpeed, entitieModel.CreatureHorizontalMovementDirection);
    }
    public WalkState(Entitie _entitieModel) : base(_entitieModel) { }
}
