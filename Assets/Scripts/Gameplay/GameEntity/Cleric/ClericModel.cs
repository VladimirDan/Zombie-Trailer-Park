using Gameplay.GameEntity.Base;
using Gameplay.GameEntity.StateMachine.States;
using UnityEngine;
using Game.Code.Common.CoroutineRunner;
using System.Collections;

public class ClericModel : UnitModel
{
    public override void Attack(GameObject target) 
    {
        Unit unitTarget = target.GetComponent<Unit>();
        UnitModel unitTargetModel = unitTarget.unitModel;

        target.gameObject.layer = (int)Mathf.Log(unitTargetModel.OpponentLayer.value, 2);
        target.transform.Rotate(0, 180, 0);
        unitTarget.OpponentLayer = this.OpponentLayer;
        unitTargetModel.OpponentLayer = this.OpponentLayer;
        unitTargetModel.OpponentBaseLayer = this.OpponentBaseLayer;

        unitTarget.CreatureHorizontalMovementDirection = this.CreatureHorizontalMovementDirection;
        unitTargetModel.CreatureHorizontalMovementDirection = this.CreatureHorizontalMovementDirection;
        
        // Vector3 scale = unitTarget.transform.localScale;
        // scale.x = -scale.x;
        // unitTarget.transform.localScale = scale;

        unitTarget.stateMachine.ChangeCurrentState(new UnitIdleState(unitTargetModel, unitTarget.coroutineRunner));
    }

    public override GameObject FindOpponent(float range)
    {
        GameObject opponent;
        float width = range;
        float depth = 10f;
        float height = 1f;

        Vector3 boxCenter = EntityTransform.position +
                            (Vector3.right * CreatureHorizontalMovementDirection * (width / 2)) +
                            (Vector3.forward * (depth / 2));

        Collider[] colliders = Physics.OverlapBox(boxCenter, new Vector3(width, height, depth), Quaternion.identity, OpponentLayer);

        if (colliders.Length != 0)
        {
            opponent = colliders.FindNearestCollider(EntityTransform).gameObject;
            if (opponent.GetComponent<BaseEntity>() != null)
            {
                opponent = null;
            }
        }

        else
        {
            opponent = null;
        }

        return opponent;
    }
    
    public override IEnumerator Fight()
    {
        GameObject target = FindOpponent(AttackRange);
        
        yield return new WaitForSeconds(AttackCooldown);

        while (true)
        {
            if(target == null && isEnemyInAttackRange())
            {
                target = FindOpponent(AttackRange);
                Attack(target);
            }
            else if (target != null && (1 << target.layer & OpponentLayer) != 0)
            {
                Attack(target);
            }
            else
            {
                yield break;
            }
            yield return new WaitForSeconds(AttackCooldown);
        }
    }
}

