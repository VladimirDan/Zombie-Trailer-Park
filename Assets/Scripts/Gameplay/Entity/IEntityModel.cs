using Assets.Scripts.StateMachine.States;
using CreaturesData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntityModel
{
    public float CreatureSpeed { get; set; }
    public float CreatureHorizontalMovementDirection { get; set; }
    public float AttackRange { get; set; }
    public float AttackDamage { get; set; }
    public float AttackSpeed { get; set; }
    public LayerMask OpponentLayer { get; set; }
    public Rigidbody EntityRigidbody { get; set; }
    public Transform EntityTransform { get; set; }

    public void Walk(float speed, float movementDirection);
    public GameObject FindOpponent();
    public bool isEnemyInAttackRange();
    public void Attack(GameObject target);
    public IEnumerator Fight();

}


