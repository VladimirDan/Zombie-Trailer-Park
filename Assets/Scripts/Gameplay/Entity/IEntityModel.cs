using Assets.Scripts.StateMachine.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntityModel
{
    float CreatureSpeed { get; set; }
    float CreatureHorizontalMovementDirection { get; set; }
    float AttackRange { get; set; }
    float AttackDamage { get; set; }
    float AttackSpeed { get; set; }
    public LayerMask OpponentLayer { get; set; }
    public Rigidbody EntityRigidbody { get; set; }
    public Transform EntityTransform { get; set; }

    public void Walk(float speed, float movementDirection);
    public GameObject FindOpponent();
    public bool isEnemyInAttackRange();
    public void Attack(GameObject target);
    public IEnumerator Fight();

}


