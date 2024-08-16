using Assets.Scripts.StateMachine.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using static HealthModel;
using static UnityEngine.EventSystems.EventTrigger;
using Unity.VisualScripting;

public class EntityModel : IEntityModel
{
    public float CreatureSpeed { get; set; }
    public float CreatureHorizontalMovementDirection { get; set; }
    public float AttackRange { get; set; }
    public float AttackDamage { get; set; }
    public float AttackSpeed { get; set; }
    public LayerMask OpponentLayer { get; set; }
    public Rigidbody EntityRigidbody { get; set; }
    public Transform EntityTransform { get; set; }

    public void Walk(float speed, float movementDirection)
    {
        Vector3 currentVelocity = new Vector3(movementDirection, EntityRigidbody.velocity.y, EntityRigidbody.velocity.z);
        EntityRigidbody.velocity = currentVelocity * speed;
    }

    public GameObject FindOpponent()
    {
        GameObject opponent;
        float width = AttackRange;
        float depth = 10f;
        float height = 1f;

        Vector3 boxCenter = EntityTransform.position +
                            (Vector3.right * CreatureHorizontalMovementDirection * (width / 2)) +
                            (Vector3.forward * (depth / 2));

        Collider[] colliders = Physics.OverlapBox(boxCenter, new Vector3(width, height, depth), Quaternion.identity, OpponentLayer);

        opponent = colliders.Length == 0 ? null : colliders.FindNearestCollider(EntityTransform).gameObject;

        return opponent;
    }

    public bool isEnemyInAttackRange()
    {
        return FindOpponent() != null;
    }

    public void Attack(GameObject target) 
    {
        HealthModel opponentHealth = target.GetComponent<HealthModel>();

        opponentHealth.ReduceHealth(AttackDamage);
    }

    public IEnumerator Fight()
    {
        GameObject target = FindOpponent();
        yield return new WaitForSeconds(AttackSpeed);

        while (true)
        {
            if(target == null && isEnemyInAttackRange())
            {
                target = FindOpponent();
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

    public EntityModel(float creatureSpeed, float creatureHorizontalMovementDirection, float attackRange,
                       float attackDamage, float attackSpeed, LayerMask opponentLayer,
                       Rigidbody EntityRigidbody, Transform EntityTransform)
    {
        this.CreatureSpeed = creatureSpeed;
        this.CreatureHorizontalMovementDirection = creatureHorizontalMovementDirection;
        this.AttackRange = attackRange;
        this.AttackDamage = attackDamage;
        this.AttackSpeed = attackSpeed;
        this.OpponentLayer = opponentLayer;
        this.EntityRigidbody = EntityRigidbody;
        this.EntityTransform = EntityTransform;
    }
}
