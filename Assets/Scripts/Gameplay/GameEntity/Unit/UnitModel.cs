using Assets.Scripts.StateMachine.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using static HealthModel;
using static UnityEngine.EventSystems.EventTrigger;
using Unity.VisualScripting;
using CreaturesData;

public class UnitModel : MonoBehaviour, IUnitModel
{
    public float CreatureSpeed { get; set; }
    public float CreatureHorizontalMovementDirection { get; set; }
    public float AttackRange { get; set; }
    public float AttackDamage { get; set; }
    public float AttackSpeed { get; set; }
    public LayerMask OpponentLayer { get; set; }
    public LayerMask OpponentBaseLayer{ get; set; }
    public Rigidbody EntityRigidbody { get; set; }
    public Transform EntityTransform { get; set; }

    public void Walk(float speed, float movementDirection)
    {
        Vector3 currentVelocity = new Vector3(movementDirection, EntityRigidbody.velocity.y, EntityRigidbody.velocity.z);
        EntityRigidbody.velocity = currentVelocity * speed;
    }

    public virtual GameObject FindOpponent(float range)
    {
        GameObject opponent;
        float width = range;
        float depth = 10f;
        float height = 1f;
        
        Vector3 rightEdgeBottomCenter = new Vector3(
            EntityTransform.position.x + (EntityTransform.localScale.x / 2 * CreatureHorizontalMovementDirection), // Правый край
            EntityTransform.position.y - (EntityTransform.localScale.y / 2), // Нижний край
            EntityTransform.position.z                                      // Позиция по Z
        );
        
        Vector3 boxCenter = rightEdgeBottomCenter +
                            (Vector3.right * CreatureHorizontalMovementDirection * (width / 2)) +
                            (Vector3.forward * (depth / 2));

        Collider[] opponentUnitsColliders = Physics.OverlapBox(boxCenter, new Vector3(width, height, depth), Quaternion.identity, OpponentLayer);
        Collider[] opponentBaseColliders = Physics.OverlapBox(boxCenter, new Vector3(width, height, depth), Quaternion.identity, OpponentBaseLayer);

        if (opponentUnitsColliders.Length != 0)
        {
            opponent = opponentUnitsColliders.FindNearestCollider(EntityTransform).gameObject;
        }
        
        else if (opponentBaseColliders.Length != 0)
        {
            opponent = opponentBaseColliders.FindNearestCollider(EntityTransform).gameObject;
        }
        else
        {
            opponent = null;
        }

        return opponent;
    }

    public bool isEnemyInAttackRange()
    {
        return FindOpponent(AttackRange) != null;
    }

    public virtual void Attack(GameObject target) 
    {
        HealthModel opponentHealth = target.GetComponent<HealthModel>();

        opponentHealth.ReduceHealth(AttackDamage);
    }

    public virtual IEnumerator Fight()
    {
        GameObject target = FindOpponent(AttackRange);
        yield return new WaitForSeconds(AttackSpeed);

        while (true)
        {
            if(target == null && isEnemyInAttackRange())
            {
                target = FindOpponent(AttackRange);
                Attack(target);
            }
            else if (target != null)
            {
                Attack(target);
            }
            else
            {
                yield break;
            }
            yield return new WaitForSeconds(AttackSpeed);
        }
    }
}
