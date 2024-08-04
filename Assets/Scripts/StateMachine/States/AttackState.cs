using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AttackState : EntitieBehaviourState
{

    public override void Enter()
    {
        Vector2 direction = new Vector2(entitieModel.CreatureHorizontalMovementDirection, entitieModel.transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast((Vector2)entitieModel.transform.position, direction, entitieModel.AttackRange, entitieModel.OpponentLayer);

        entitieModel.attackCoroutine = entitieModel.StartCoroutine(entitieModel.Attack(hit));
    }

    public override void Exit()
    {
        if(entitieModel.attackCoroutine != null)
        {
            entitieModel.StopCoroutine(entitieModel.attackCoroutine);
            entitieModel.attackCoroutine = null;
        }
    }

    public AttackState(Entitie _entitieModel) : base(_entitieModel) { }
}
